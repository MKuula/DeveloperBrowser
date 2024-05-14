# Developer's Browser
Developer's Browser is a web browser aimed towards web developers who are testing their hypertext pages and scripts. It is a quite primitive browser, and not meant to replace your current browser. Supposing that this application starts on your machine, you ought to have Microsoft Edge installed anyway (as it uses Chromium-based engine).

## Getting started
![image](https://github.com/MKuula/DeveloperBrowser/assets/168563015/bc8945f7-3477-4705-8c30-0658d8034e45)

You have several ways to open a desired file into the browser. You can either
- double click the address located in the middle to open file dialog.
- click the folder icon beside the address bar to do the same.
- type the filename (and path) directly into the address bar (remember the file protocol prefix `file:///`).

At the left side are normal navigation controls (go to previous or next page, reload and stop).

## Features
Currently, the only developer-specific feature is automatic reload. Changes to _the current file or any files in the same directory or subdirectory(ies)_ causes the document to be reloaded. This is the same effect that can be achieved by using Live Server (or similar plugin), but without the need to use Visual Code (at this moment that plugin is not available on Visual Studio).

![image](https://github.com/MKuula/DeveloperBrowser/assets/168563015/8ec08a15-1d20-411a-9fcb-1015eb4d322a)

The auto-reload feature can be toggled on or off with the eye icon. It automatically turns on upon loading a new document.

> [!NOTE]
> This version watches only hypertext files (with prefix .html/.htm), stylesheets (.css) and javascript code files (.js). So, if you want other file types under watchful eye, you need to tweak the code. Perhaps next version has customizable options...

---

![image](https://github.com/MKuula/DeveloperBrowser/assets/168563015/f4515271-b4d2-49c5-97fb-cfda422087ed) Tools button opens developer tools without the need to mess with menus.
