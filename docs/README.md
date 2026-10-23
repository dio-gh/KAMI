# KAMI website

A small Jekyll site for GitHub Pages: one layout, one stylesheet, a landing page and a 404 page. No browser JavaScript, analytics, external fonts, frontend package manager or third-party theme.

## Publish on GitHub Pages

In **Settings → Pages**, select **Deploy from a branch**, choose the branch containing this directory, and select **/docs**. GitHub builds Jekyll automatically; no new Actions workflow is required. Preview on a fork before moving to the upstream repository.

`baseurl: /KAMI` is correct for a project repository named KAMI, including a fork. Change it if the repository name changes. Use an empty baseurl for a root user/organization site or root custom domain. GitHub Pages supplies the site origin for the hosting repository; a custom host can set `url` explicitly.

All internal links use Jekyll's URL filters. Do not add `.nojekyll`: that would bypass Liquid and layout processing. The existing compatibility documentation at `https://isjuhn.github.io/` is linked separately.

## Edit content

- `index.html`: landing copy, screenshot and getting-started notes.
- `_config.yml`: description, base path and project/download/documentation links.
- `_layouts/default.html`: metadata, header and footer for future pages.
- `assets/css/site.css`: styling and responsive layouts.
- `assets/images/app-preview.png`: screenshot, explicitly labeled as work in progress.

The download button opens the latest GitHub release. There is no hard-coded version or asset filename to update; the button does not download a file automatically. Requirements and supported emulators come from the project README and should be revisited when releases change them.

The screenshot's OS title bar is clipped with CSS. The file is otherwise unchanged and has no ancillary text metadata. Replace it and remove that CSS rule when a clean application-only capture is available. No game artwork or emulator logos are bundled.

Add future content as Markdown with YAML front matter such as `title: Setup`. The default layout is inherited from `_config.yml`. Add navigation when those pages have useful content.

## Optional local preview

With an existing Ruby/Bundler installation, review `Gemfile`, then run `bundle install` and `bundle exec jekyll serve` in this directory. Dependency installation is a maintainer action; it is not needed for GitHub's hosted build. Commit the resulting `Gemfile.lock` if local versions need to be frozen for a team. GitHub manages the hosted Jekyll dependency versions.

No Ruby, gem or npm package was installed locally during authoring.

## Release checks

Check the deployed landing and 404 pages, project-path asset URLs, mobile layouts, keyboard focus and outbound links. Keep the screenshot's development label until it depicts the release being downloaded.

Before committing, inspect the intended website files for personal paths, real names, private email addresses, credentials and image metadata. Use a public GitHub handle and its noreply email for commit attribution. Keep local preview tools, browser profiles and build logs out of this directory.
