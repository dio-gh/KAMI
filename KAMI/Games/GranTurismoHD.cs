using KAMI.Games;
using KAMI.Cameras;
using KAMI.Utilities;
using System;

namespace KAMI
{
    class GranTurismoHD : Game<HAVACamera>
    {
        const uint m_current_cur_pos_x_addr = 0x32A26CE4;
        const uint m_current_cur_pos_y_addr = m_current_cur_pos_x_addr + 0x4;
        const uint m_target_cur_pos_x_addr = 0x32A28310;
        const uint m_target_cur_pos_y_addr = m_target_cur_pos_x_addr + 0xC;

        const uint m_bbox_left_addr = 0x32A282F4;
        const uint m_bbox_top_addr = m_bbox_left_addr + 0x4;
        const uint m_bbox_right_addr = m_bbox_left_addr + 0x8;
        const uint m_bbox_bottom_addr = m_bbox_left_addr + 0xC;

        const uint m_mainmenu_sel_target = 0x32598348;
        const uint m_mainmenu_sel_current = m_mainmenu_sel_target + 0x4;

        float cur_x;
        float cur_y;

        float bbox_top;
        float bbox_bottom;
        float bbox_left;
        float bbox_right;

        uint selection;

        public GranTurismoHD(IntPtr ipc) : base(ipc)
        {
        }

        public override void InjectionStart()
        {
            // cursor pos readout
            cur_x = IPCUtils.ReadFloat(m_ipc, m_current_cur_pos_x_addr);
            cur_y = IPCUtils.ReadFloat(m_ipc, m_current_cur_pos_y_addr);

            // menu item selection readout
            selection = IPCUtils.ReadU32(m_ipc, m_mainmenu_sel_target);
        }

        public override void UpdateCamera(int diffX, int diffY)
        {
            // bounding box readout
            bbox_top = IPCUtils.ReadFloat(m_ipc, m_bbox_top_addr);
            bbox_bottom = IPCUtils.ReadFloat(m_ipc, m_bbox_bottom_addr);
            bbox_left = IPCUtils.ReadFloat(m_ipc, m_bbox_left_addr);
            bbox_right = IPCUtils.ReadFloat(m_ipc, m_bbox_right_addr);

            // cursor pos update
            cur_x += diffX;
            cur_y += diffY;
            
            // cursor pos writeback
            IPCUtils.WriteFloat(m_ipc, m_current_cur_pos_x_addr, cur_x);
            IPCUtils.WriteFloat(m_ipc, m_target_cur_pos_x_addr, cur_x);
            IPCUtils.WriteFloat(m_ipc, m_current_cur_pos_y_addr, cur_y);
            IPCUtils.WriteFloat(m_ipc, m_target_cur_pos_y_addr, cur_y);

            // menu item selection handling
            // NOTE: for the main menu demo only
            Console.WriteLine($"BBOX L: '{bbox_left}', BBOX R: '{bbox_right}', X: '{cur_x}'");
            if (cur_x >= bbox_left && cur_x <= bbox_right)
            {
                Console.WriteLine($"BBOX T: '{bbox_top}', BBOX B: '{bbox_bottom}', Y: '{cur_y}'");
                // menu item selection update
                if (cur_y < bbox_top) selection--;
                else if (cur_y > bbox_bottom) selection++;

                // menu item selection writeback
                if (selection >= 0 && selection <= 5)
                {
                    Console.WriteLine("Selected element idx: " + selection);
                    IPCUtils.WriteU32(m_ipc, m_mainmenu_sel_current, selection);
                    IPCUtils.WriteU32(m_ipc, m_mainmenu_sel_target, selection);
                }
            }
        }
    }
}