# Installation & Setup:

## What is required for this installation(all links are dirrect DL links):
1. [.net 6 SDk(x64)](<https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-6.0.420-windows-x64-installer>)

2. [CleanFlash](<https://drive.google.com/file/d/1R0KrjAyHTz6KfcAp_zsRh2Mrv25J0PbI/view>) (and for it to be installed properly according to this: [CleanFlash Installation](https://imgur.com/ztsLYZ1))

3. [Skua-1.2.4.0-Release-x64.msi](<https://github.com/BrenoHenrike/Skua/releases/download/1.2.4.0/Skua-1.2.4.0-Release-x64.msi>)

- install either of the `.net 6 sdk` or `CleanFlash` before `Skua`.

## Once everything is properly installed:

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
                - [Enable/Disable] the use of Boosts during scripts if called for (Boosters include: Gold/Class/Reputation/Experience)
            2. Nation Farms
               * [Enable/Disabled] Sell Voucher of Nulgath if not needed (during scripts/farming things such as `Supplies to spin the wheel`, if this is checked it will sell the `Voucher of Nulgath(member)`, when its not the item its going for.)
               * [Enable/Disabled] Do Swindles Return during Supplies (Wether it should do the quest `Swindles Return` during Scripts / farming methods such as `Supplies to spin the wheel`)
            3. Bludrut Brawl (PVP) 
               * (this is deprecated[no longer used], as killing all mobs returns 10 trophies rather then a measily 2..or 3 i do not remmbe. )
            4. Bot Creatures Only
               * **you will not use this unless told by a dev** 


# Basic Functionality and a look around Skua

1. `Scripts` Button:
   - **Explained Above!**
2. `Options` Button:
    - `Game Options`
        *  These options are explain above in the `Once everything is properly installed` section.
    - `Application Options`
        * these Optons are for if u dont want your `Scripts` or `SkillSets` to be Update / reset (if you've changed then.)
    - `CoreBots`
        *  These options are explain above in the `Once everything is properly installed` section.
    - `Application Theme`
        * This is where you can set the `Theme` of the Skua Windows.
    - `HotKeys`
        * this is where you can change the [Defaulted] hotkeys.
3. `Helpers`
    - `RunTime`
      * This is where you can:
        1.  **Manualy** set up Quests to automaticly be accepted and turned in
        2.  Add drops to the droplist, to be picked up
        3. [Enable/Disable] accepting of ac drops / rejecting things that *arent* in the drop list.
        4. [Enable/Disable] Usage of Boosts (if you arent currently running a script.)
    - `Fast Travel`
      * [Add/Use] Preset / Custom Made *Fast Travel points* to get Around AQW
    - `Current Drops`
     * This is where the Rejected Drops (as long as you havent been [Disconnected/Loggedout] will be for you to pickup if u missed someting you want.)
4. `Tools`
    - `Loader`
        1. Load Shops (most require you to be in the required map for the shop.)
        2. [Load/Searchfor] Quests
    - `Grabber` **Pressing Grab on any of these tabs will Get the Required information for the `grabber`**
        1. Shop Items - Gets the item of the currently loaded shop
        2. Shop IDs - gets the ShopID's of the shops that have been loaded *so far* this login session.
        3. Quests - shows the QuestData for the quets that have been loaded in thh current session
        4. Inventory - All the current item in your `Inventory`
        5. House Inventory - All the current item in your `House Inventory`
        6. Temp Inventory - All the current item in your `TempInventory` (mostly Quest Items)
        7. Bank Items - All the current item in your `Bank`
        8. Cell Monsters - Data for All the Mobs in the current Cell (the room your in)
        9. Map Mosnters - Data for All the Mobs in the current Map
        10. GetMap Item IDs (may or may not work) - this is supposed to get the Quests Items that are clickable on the map.. though its a bit wonky.
    - `Status` - Shows the Current (unless reset) kills, deaths, Quests [Accpeted/Compelted], pickups, relogins, and Current session time.
    - `Consol` - **you will not use this unless told by a dev** 
5. `Skills`
    - this is where you can edit our *PreMade* `SKillsets` to your linking (skill #'s are -1 what they are ingame so: 1-2-3-4-5-6 = 0(not useable)-1-2-3-4-5(due to how potions/use items work this usualy doesnt work well))
6. `Packets`
    - **you will not use this unless told by a dev** 
7. `Bank`
    - Will Load your bank (can also be brought up by pressing `B`)
8. `Logs` - (preferably the `Scripts` tab)
    - Usualy what the devs will ask for if you're having script issues (before crying to a script dev, please do these; 
      1. Relog & restart the script
      2. in the 3rd tab of the `Skua Manaber` under `Updates`, under `Scripts`, press `Reset Scripts`
      3. you may now come to us with your issue and provide screenshots and a good explanation of teh issue.
9.  `Plugins`
    - this will be Empty as of Currently (unless you have the `Cosmetics Plugin[Beta]` by Lord Exelot [CosmeticsPlugin](<https://drive.google.com/file/d/1scL9o5bgaQLNZe-dRwrZbS-LOKx4jKeR/view?usp=share_link>))
    ### How to install the Cosmetics Plugin for Skua
    1. Download the .dll file from the link in the pins.
    2. Locate the file on your PC (usually in your /Downloads/ folder).
    3. Copy (ctrl+c) or Cut (ctrl+x) the .dll file.
    4. Go to the Skua plugin directory in your Documents (example: C:\Users\YourName\Documents\Skua\plugins\)
    5. Paste (ctrl+v) the file.
    6. Restart any Skua instances so you can see the plugin.
    7. Within the Skua window, go to plugins (last option from the left) and then Cosmetics Plugin.
    8. Repeat step 7 every time you wish to access the plugin.


For futher information or questions, Join the [Discord](https://discord.gg/pearlharbor)
