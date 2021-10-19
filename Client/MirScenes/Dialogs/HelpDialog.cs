using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.MirControls;
using Client.MirGraphics;
using Client.MirNetwork;
using Client.MirObjects;
using Client.MirSounds;
using S = ServerPackets;
using C = ClientPackets;

namespace Client.MirScenes.Dialogs
{
    public sealed class HelpDialog : MirImageControl
    {
        public List<HelpPage> Pages = new List<HelpPage>();

        public MirButton CloseButton, NextButton, PreviousButton;
        public MirLabel PageLabel;
        public HelpPage CurrentPage;

        public int CurrentPageNumber = 0;

        public HelpDialog()
        {
            Index = 920;
            Library = Libraries.Prguse;
            Movable = true;
            Sort = true;

            Location = Center;

            MirImageControl TitleLabel = new MirImageControl
            {
                Index = 57,
                Library = Libraries.Title,
                Location = new Point(18, 9),
                Parent = this
            };

            PreviousButton = new MirButton
            {
                Index = 240,
                HoverIndex = 241,
                PressedIndex = 242,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(210, 485),
                Sound = SoundList.ButtonA,
            };
            PreviousButton.Click += (o, e) =>
            {
                CurrentPageNumber--;

                if (CurrentPageNumber < 0) CurrentPageNumber = Pages.Count - 1;

                DisplayPage(CurrentPageNumber);
            };

            NextButton = new MirButton
            {
                Index = 243,
                HoverIndex = 244,
                PressedIndex = 245,
                Library = Libraries.Prguse2,
                Parent = this,
                Size = new Size(16, 16),
                Location = new Point(310, 485),
                Sound = SoundList.ButtonA,
            };
            NextButton.Click += (o, e) =>
            {
                CurrentPageNumber++;

                if (CurrentPageNumber > Pages.Count - 1) CurrentPageNumber = 0;

                DisplayPage(CurrentPageNumber);
            };

            PageLabel = new MirLabel
            {
                Text = "",
                Font = new Font(Settings.FontName, 9F),
                DrawFormat = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter,
                Parent = this,
                NotControl = true,
                Location = new Point(230, 480),
                Size = new Size(80, 20)
            };

            CloseButton = new MirButton
            {
                HoverIndex = 361,
                Index = 360,
                Location = new Point(509, 3),
                Library = Libraries.Prguse2,
                Parent = this,
                PressedIndex = 362,
                Sound = SoundList.ButtonA,
            };
            CloseButton.Click += (o, e) => Hide();

            LoadImagePages();

            DisplayPage();
        }

        private void LoadImagePages()
        {
            Point location = new Point(12, 35);

            Dictionary<string, string> keybinds = new Dictionary<string, string>();

            List<HelpPage> imagePages = new List<HelpPage> { 
                new HelpPage(Resources.ResourceHelpDialog.ShortcutInformation, -1, new ShortcutPage1 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(Resources.ResourceHelpDialog.ShortcutInformation, -1, new ShortcutPage2 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(Resources.ResourceHelpDialog.ChatShortcuts, -1, new ShortcutPage3 { Parent = this } ) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(Resources.ResourceHelpDialog.Movements, 0, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(Resources.ResourceHelpDialog.Attacking, 1, null) { Parent = this, Location = location, Visible = false }, 
                new HelpPage(Resources.ResourceHelpDialog.CollectingItems, 2, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Health, 3, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Skills, 4, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Skills, 5, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Mana, 6, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Chatting, 7, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Groups, 8, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Durability, 9, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Purchasing, 10, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Selling, 11, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Repairing, 12, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Trading, 13, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Inspecting, 14, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Statistics, 15, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Statistics, 16, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Statistics, 17, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Statistics, 18, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Statistics, 19, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Statistics, 20, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Quests, 21, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Quests, 22, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Quests, 23, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Quests, 24, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Mounts, 25, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Mounts, 26, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.Fishing, 27, null) { Parent = this, Location = location, Visible = false },
                new HelpPage(Resources.ResourceHelpDialog.GemsAndOrbs, 28, null) { Parent = this, Location = location, Visible = false },
            };

            Pages.AddRange(imagePages);
        }


        public void DisplayPage(string pageName)
        {
            if (Pages.Count < 1) return;

            for (int i = 0; i < Pages.Count; i++)
            {
                if (Pages[i].Title.ToLower() != pageName.ToLower()) continue;

                DisplayPage(i);
                break;
            }
        }

        public void DisplayPage(int id = 0)
        {
            if (Pages.Count < 1) return;

            if (id > Pages.Count - 1) id = Pages.Count - 1;
            if (id < 0) id = 0;

            if (CurrentPage != null)
            {
                CurrentPage.Visible = false;
                if (CurrentPage.Page != null) CurrentPage.Page.Visible = false;
            }

            CurrentPage = Pages[id];

            if (CurrentPage == null) return;

            CurrentPage.Visible = true;
            if (CurrentPage.Page != null) CurrentPage.Page.Visible = true;
            CurrentPageNumber = id;

            CurrentPage.PageTitleLabel.Text = id + 1 + ". " + CurrentPage.Title;

            PageLabel.Text = string.Format("{0} / {1}", id + 1, Pages.Count);

            Show();
        }


        public void Toggle()
        {
            if (!Visible)
                Show();
            else
                Hide();
        }
    }

    public class ShortcutPage1 : ShortcutInfoPage
    {
        public ShortcutPage1()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Exit), Resources.ResourceShortcuts.ExitTheGame),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Logout), Resources.ResourceShortcuts.LogOut),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill1) + "-" + CMain.InputKeys.GetKey(KeybindOptions.Bar1Skill8), Resources.ResourceShortcuts.SkillButtons),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Inventory), Resources.ResourceShortcuts.InventoryWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Equipment), Resources.ResourceShortcuts.StatusWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skills), Resources.ResourceShortcuts.SkillWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Group), Resources.ResourceShortcuts.GroupWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Trade), Resources.ResourceShortcuts.TradeWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Friends), Resources.ResourceShortcuts.FriendWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Minimap), Resources.ResourceShortcuts.MinimapWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Guilds), Resources.ResourceShortcuts.GuildWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.GameShop), Resources.ResourceShortcuts.GameshopWindowOpenClose),
                //Shortcuts.Add(new ShortcutInfo("K", "Rental window (open / close)"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Relationship), Resources.ResourceShortcuts.EngagementWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Belt), Resources.ResourceShortcuts.BeltWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Options), Resources.ResourceShortcuts.OptionWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Help), Resources.ResourceShortcuts.HelpWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mount), Resources.ResourceShortcuts.MountDismounRide)
            };

            LoadKeyBinds();
        }
    }
    public class ShortcutPage2 : ShortcutInfoPage
    {
        public ShortcutPage2()
        {
            Shortcuts = new List<ShortcutInfo>
            {
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangePetmode), Resources.ResourceShortcuts.TogglePetAttackMode),
                //Shortcuts.Add(new ShortcutInfo("Ctrl + F", "Change the font in the chat box"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.ChangeAttackmode), Resources.ResourceShortcuts.TogglePlayerAttackMode),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodePeace), Resources.ResourceShortcuts.PeaceModeAttackMonstersOnly),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGroup), Resources.ResourceShortcuts.GroupModeAttackAllSubjectsExceptYourGroupMembers),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeGuild), Resources.ResourceShortcuts.GuildModeAttackAllSubjectsExceptYourGuildMembers),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeRedbrown), Resources.ResourceShortcuts.GoodEvilModeAttackPKPlayersAndMonstersOnly),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.AttackmodeAll), Resources.ResourceShortcuts.AllAttackModeAttackAllSubjects),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Bigmap), Resources.ResourceShortcuts.ShowTheFieldMap),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Skillbar), Resources.ResourceShortcuts.ShowTheSkillBar),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Autorun), Resources.ResourceShortcuts.AutoRunOnOff),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Cameramode), Resources.ResourceShortcuts.ShowHideInterface),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Pickup), Resources.ResourceShortcuts.HighlightPickupItems),
                new ShortcutInfo("Ctrl + "+Resources.ResourceShortcuts.RightClick, Resources.ResourceShortcuts.ShowOtherPlayersKits),
                //Shortcuts.Add(new ShortcutInfo("F12", "Chat macros"));
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Screenshot), Resources.ResourceShortcuts.ScreenCapture),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Fishing), Resources.ResourceShortcuts.FishingWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.Mentor), Resources.ResourceShortcuts.MentorWindowOpenClose),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreaturePickup), Resources.ResourceShortcuts.CreaturePickupMultiMouseTarget),
                new ShortcutInfo(CMain.InputKeys.GetKey(KeybindOptions.CreatureAutoPickup), Resources.ResourceShortcuts.CreaturePickupSingleMouseTarget)
            };

            LoadKeyBinds();
        }
    }
    public class ShortcutPage3 : ShortcutInfoPage
    {
        public ShortcutPage3()
        {
            Shortcuts = new List<ShortcutInfo>();
            //Shortcuts.Add(new ShortcutInfo("` / Ctrl", "Change the skill bar"));
            Shortcuts.Add(new ShortcutInfo(string.Format("/({0})",Resources.ResourceShortcuts.Username), Resources.ResourceShortcuts.CommandToWhisperToOthers));
            Shortcuts.Add(new ShortcutInfo(string.Format("!({0})", Resources.ResourceShortcuts.Text), Resources.ResourceShortcuts.CommandToShoutToOthersNearby));
            Shortcuts.Add(new ShortcutInfo(string.Format("!~({0})",Resources.ResourceShortcuts.Text), Resources.ResourceShortcuts.CommandToGuildChat));

            LoadKeyBinds();
        }
    }

    public class ShortcutInfo
    {
        public string Shortcut { get; set; }
        public string Information { get; set; }

        public ShortcutInfo(string shortcut, string info)
        {
            Shortcut = shortcut.Replace("\n", " + ");
            Information = info;
        }
    }

    public class ShortcutInfoPage : MirControl
    {
        protected List<ShortcutInfo> Shortcuts = new List<ShortcutInfo>();

        public ShortcutInfoPage()
        {
            Visible = false;

            MirLabel shortcutTitleLabel = new MirLabel
            {
                Text = Resources.ResourceShortcuts.Shortcuts,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(13, 75),
                Size = new Size(100, 30)
            };

            MirLabel infoTitleLabel = new MirLabel
            {
                Text = Resources.ResourceShortcuts.Information,
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                ForeColour = Color.White,
                Font = new Font(Settings.FontName, 10F),
                Parent = this,
                AutoSize = true,
                Location = new Point(114, 75),
                Size = new Size(405, 30)
            };
        }

        public void LoadKeyBinds()
        {
            if (Shortcuts == null) return;

            for (int i = 0; i < Shortcuts.Count; i++)
            {
                MirLabel shortcutLabel = new MirLabel
                {
                    Text = Shortcuts[i].Shortcut,
                    ForeColour = Color.Yellow,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(18, 107 + (20 * i)),
                    Size = new Size(95, 23),
                };

                MirLabel informationLabel = new MirLabel
                {
                    Text = Shortcuts[i].Information,
                    DrawFormat = TextFormatFlags.VerticalCenter,
                    ForeColour = Color.White,
                    Font = new Font(Settings.FontName, 9F),
                    Parent = this,
                    AutoSize = true,
                    Location = new Point(119, 107 + (20 * i)),
                    Size = new Size(400, 23),
                };
            }  
        }
    }

    public class HelpPage : MirControl
    {
        public string Title;
        public int ImageID;
        public MirControl Page;

        public MirLabel PageTitleLabel;

        public HelpPage(string title, int imageID, MirControl page)
        {
            Title = title;
            ImageID = imageID;
            Page = page;

            NotControl = true;
            Size = new System.Drawing.Size(508, 396 + 40);

            BeforeDraw += HelpPage_BeforeDraw;

            PageTitleLabel = new MirLabel
            {
                Text = Title,
                Font = new Font(Settings.FontName, 10F, FontStyle.Bold),
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
                Parent = this,
                Size = new System.Drawing.Size(242, 30),
                Location = new Point(135, 4)
            };
        }

        void HelpPage_BeforeDraw(object sender, EventArgs e)
        {
            if (ImageID < 0) return;

            Libraries.Help.Draw(ImageID, new Point(DisplayLocation.X, DisplayLocation.Y + 40), Color.White, false);
        }
    }
}
