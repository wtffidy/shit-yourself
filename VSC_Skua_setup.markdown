VScode Setup for: `Skua`
----------------------------
Things you'll need [ Control + Click to view images / goto links.]:

# Links: 

- .net 6 SDK +
[.net 6 SDk(Dirrect link)](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.420-windows-x64-installer)

- VSCode
[VSCode](https://code.visualstudio.com/download)

- Scripts Git(to setup Github Desktop later on)
[Scripts Git](https://github.com/BrenoHenrike/Scripts.git)

- csproj file for use later
[csproj file](https://github.com/wtffidy/shit-yourself/blob/main/Scripts.csproj)

- GitHub Desktop
[Github Desktop](https://desktop.github.com/)


# VSC Terminal Commands (open terminal with control key + `[below escape key]) [in order]:

1. `dotnet new classlib` [add --force if it asks, ex; `dotnet new classlib --force`]
2. `dotnet add package Newtonsoft.Json`
3. `dotnet add package CommunityToolkit.Mvvm --version 8.2.2`


# Setup Steps [From *after* `Skua` installation]:

0. Download all afformentioned items.
00. if `Skua` / `Skua Manager` is open in the system tray (` bottom right`), please make sure it is closed **FULLY**.

1. Launch `Skua`

2. Wait a moment for the scripts to Download themselves

3. On the`Skua` Menu bar (all the buttons at the top), click `options`, and then `application`

4. Uncheck all those options (`you may leave the skill options on if want.`)

5. *Close* `Skua`, open `File Explorer`, navigate to navigate to: `C:\Users\User\Documents\Skua`, delete the `Scripts` folder.

6. with the folder now gone, open `Github Desktop`, File > Clone Repo. > Tab 3 [#URL]
    - Under Repository URL paste: `https://github.com/BrenoHenrike/Scripts.git`
    - Under Local path; **navigate to: > `C:\Users\User\Documents\Skua**, and **Select Folder** in the bottom right.
    - ![Example(*User* is a place holder)](https://i.imgur.com/SP4OBNZ.png) 

7. Close Github Desktop (this is where you will get your updates now [just hit "Fetch Origin" ![Fetch Origin](https://i.imgur.com/J6vMvle.png)]) and itll automaticly pull the updates (same as if u where to hit get scripts > update / reset scripts through the client/manager).

8. Open VScode and do the following;

- First we're going to Install some Extentions;
Under `Extentions`, we'll need to Install These 2 (just search for them):
        - `ms-dotnettools.csharp` 
 
- Secondly: `control + k, followed by control + o (to select a folder)` > navigate to: > `C:\Users\User\Documents\Skua`, click on the `Scripts` folder, and in he bottom right click `Select Folder`

On the Right hand side, you'll see several Icons ![VSC Icons](https://i.imgur.com/l5nbsFf.png).
In order[of the image]: 
    - Explorer
    - Search
    - Source Control (Not usfull to you.)
    - Run and Debug (Not usfull to you.)
    - Extensions (we'll come back to this)
    - Git Lese (you may not have this.)
    - Additonal Views (Not usfull to you.)

9. With the extention installed, and scripts folder opened inside VScode:
    - We'll need to run the Following Cmds mentioned on Line 20 Above
        - as a reminder: 
            1. `dotnet new classlib` [add --force if it asks, ex; `dotnet new classlib --force`]
                - this generates the `Scripts.csproj` that we'll be replacing.
            2. `dotnet add package Newtonsoft.Json`
            3. `dotnet add package CommunityToolkit.Mvvm --version 8.2.2`
        - Press: `control + shift + p`, type: "Restore" find: ![Restore All Projects](https://i.imgur.com/yZ3xbzh.png), click it / down arrow and press enter.
        - give it a moment

    - With the `Scripts.csproj` file Generated, and approriate packages added, we can now replace this file with: [Scripts.csproj](https://github.com/wtffidy/shit-yourself/blob/main/Scripts.csproj), just copy the text from there, and replace all inside the `Scripts.csproj` file.

 once again:
 - Press: `control + shift + p`, type: "Restore" find: ![Restore All ProJcets](https://i.imgur.com/yZ3xbzh.png), click it / down arrow and press enter.
        - give it a moment

 - Press: `control + shift + p` [again], type: "Restart" find:   ![Restart OmniSharp](https://i.imgur.com/boCTkJp.png), click it / down arrow and press enter.
        - give it a moment

# *IF* everything went according to plan, VSCode should now be setup for Skua scripting.
We can test this by doing the following:
open a test file: `Templates\Examples\ItemGathering\EmptyTemplate[Basics].cs` for example, goto a *new* line within the public void (just make a new line below the `Core.CancelRegisteredQuests();` ![New Line](https://i.imgur.com/c8hxAUb.png)), and type: `Core.` and if it brings up a context menu with the different things u can use, its done correctly, other wise, it did not work. 

![End Result](https://i.imgur.com/UO5i9Vr.png)
