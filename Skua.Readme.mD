~Installation & Setup:~

*What is required for this installation(all links are dirrect DL links):*
1. [.net 6 SDk(x64)](<https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.420-windows-x64-installer>)
2. [CleanFlash](<https://drive.google.com/file/d/1R0KrjAyHTz6KfcAp_zsRh2Mrv25J0PbI/view>) (and for it to be installed properly according to this: [CleanFlash Installation](https://imgur.com/ztsLYZ1))
3. [Skua-1.2.4.0-Release-x64.msi](<https://github.com/BrenoHenrike/Skua/releases/download/1.2.4.0/Skua-1.2.4.0-Release-x64.msi>)

- install either of the `.net 6 sdk` or `CleanFlash` before `Skua`.

*once everything is properly installed:*
1. Open `Skua` or the `Skua Manager` (these will be placed on your desktop by default.)
2. close the Changelog that appears, if skua isnt showing its more then likely in teh system tray (little `^` in the bottom right. just RightClick it and "show client").
3. with Skua opened, you can now Search for, and start scripts (simply press "scripts" button in the top left, and )
- To start a script you can do one of 2 ways:
    * Press `Get Scripts`, (if no scripts appear press the ⟳ button) and Search for / load a script from here.
    * Press `Load script`, (this will take you to the `Documents > skua > Scripts` folder. [ you can serach in the top right of the explorer window for your desired script]), once loaded press start on the `scripts` window.
4. it is strongly recomended before you start scripts to setup your `CoreBot Options` ... Options
    - this can be done by going to `Options > CoreBots`, and setting the Following:
        * Tab 1 [Loadout]:
            1. `Solo/Farm Class` & the `Mode` (meaning things such as `defence`, `Attack`, `Basic`, etc)
            2. `Solo/Farm Equipment` - this will be equiped when scripts call the `Core.EquipClass();`
        * Tab 2[Options]: 
            Really the only things you should Edit here are:
            1. Room Number
            2. [Enable/Disable] options such as: 
                - Antilag
                - Bank misc
                - Disable [AutoEnh/BestGear] (BestGear no longer being used as it causes crashes so its diabled from the core).
                - Force OFf MessageBoxes (so they dont appear during your gaming... during aprilfools they will so be warned of that ^_^ ).
                - Logger in Chat (this just disabled the script logging messages in the chat [these are only seen by you]).
                - Private Rooms (on by default and we **HIGHTLY** suggest you keep it on).
                - Public on Difficult Parts (not relay use across many scripts, but this will put you into a public instance (-1~) of w/e map its farming if enabled [once again we **HIGHTLY** suggest you keep it ***OFF***])
                - Should Rest (this option is upto you, and doesnt realy mess with anything its just spams a rest packet between combats.)
                - Map After stopping the bot (you can set this to `None` to stop where the bot does, or a `CustomName` of your choosing, or house[default] to have it stop at your player house).
        * Tab 3[Other]:
            1. Boosters
                - [Enable/Disable] the use of Boosts during scripts if called for (ToggleBoost(BoostType.Reputation);)
