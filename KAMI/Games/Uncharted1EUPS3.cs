using System;
using KAMI.Cameras;
using KAMI.Utilities;

namespace KAMI.Games
{
    class Uncharted1EUPS3 : Game<HVecVACamera>
    {
        const uint BaseAddress = 0xd00386f0;

        DerefChain m_hor;

        public Uncharted1EUPS3(IntPtr ipc) : base(ipc)
        {
            var baseChain = DerefChain.CreateDerefChain(ipc, BaseAddress, 0x71a0);
            m_hor = baseChain.Chain(0x12a0);
        }

        public override void InjectionStart()
        {
            m_camera.HorY = IPCUtils.ReadFloat(m_ipc, 0x34CC7FA0);
            m_camera.HorX = IPCUtils.ReadFloat(m_ipc, 0x34CC7FA8);
            m_camera.Vert = (float)(IPCUtils.ReadFloat(m_ipc, 0x34CC7F4C) * Math.PI);
        }

        public override void UpdateCamera(int diffX, int diffY)
        {
            if (DerefChain.VerifyChains(m_hor))
            {
                m_camera.Update(-diffX * SensModifier, diffY * SensModifier);
                m_camera.Vert = Math.Clamp(m_camera.Vert, 0.0f, (float)Math.PI);
                IPCUtils.WriteFloat(m_ipc, 0x34CC7FA0, m_camera.HorY);
                IPCUtils.WriteFloat(m_ipc, 0x34CC7FA8, m_camera.HorX);
                IPCUtils.WriteFloat(m_ipc, 0x34CC7F4C, (float)(m_camera.Vert / Math.PI));
            }
        }
    }
}
