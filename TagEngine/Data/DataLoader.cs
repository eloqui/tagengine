using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Entities;
using static TagEngine.Entities.Room;

namespace TagEngine.Data
{
	public static class DataLoader
	{
#if DEBUG
        /// <summary>
        /// Load up the hardcoded OddShip test game
        /// </summary>
        /// <returns></returns>
        public static GameState GetTestGame()
        {
            #region Rooms

            Room shuttle, qtrs1, airlock, qtrs2, corridor4, garden, corridor5, no1qtrs, capqtrs, weapons;
            Room bridge, corridor2, corridor1, corridor3, docking, engineering2, observation, engineering1;
            Room tools, mess, engine2, galley, engine1, mysterious;

            shuttle = new Room("shuttle", "Shuttlecraft", "the small shuttle craft", false);
            qtrs1 = new Room("qtrs1", "Crew's Quarters 1", "starboard crew quarters", false);
            airlock = new Room("airlock", "Airlock", "the airlock that leads to the shuttle craft", false);
            qtrs2 = new Room("qtrs2", "Crew's Quarters 2", "port crew quarters", false);
            corridor4 = new Room("corridor4", "Corridor", "a fairly dull corridor");
            garden = new Room("garden", "Garden", "the huge garden");
            corridor5 = new Room("corridor5", "Corridor", "a fairly dull corridor");
            no1qtrs = new Room("no1qtrs", "1st Officer's Quarters", "the 1st officer's quarters", false);
            capqtrs = new Room("capqtrs", "Captain's Quarters", "the captain's quarters", false);
            weapons = new Room("weapons", "Weapons Locker", "the weapons locker", false);
            bridge = new Room("bridge", "The Bridge", "the bridge: nerve-centre of the ship", false);
            corridor2 = new Room("corridor2", "Corridor", "a fairly dull corridor");
            corridor1 = new Room("corridor1", "Corridor", "a fairly dull corridor");
            corridor3 = new Room("corridor3", "Corridor", "a fairly dull corridor");
            docking = new Room("docking", "Docking Port", "the docking port");
            engineering2 = new Room("engineering2", "Starboard Engineering", "the starboard engineering room", false);
            observation = new Room("observation", "Observation Deck", "the amazing observation lounge");
            engineering1 = new Room("engineering1", "Port Engineering", "the port engineering room", false);
            tools = new Room("tools", "Tool Locker", "the tools locker", false);
            mess = new Room("mess", "The Mess", "the eating lounge");
            engine2 = new Room("engine2", "Starboard Engine", "the starboard engine", false);
            galley = new Room("galley", "The Galley", "the galley");
            engine1 = new Room("engine1", "Port Engine", "the port engine", false);
            mysterious = new Room("mysterious", "Mysterious Cavity", "a mysterious cavity", false);

            shuttle.SetExits(new Exits { { Room.Exit.East, airlock } }); // NSEW
            qtrs1.SetExits(new Exits { { Room.Exit.East, corridor4 } });
            airlock.SetExits(new Exits { { Room.Exit.East, garden }, { Room.Exit.West, shuttle } });
            qtrs2.SetExits(new Exits { { Room.Exit.East, corridor5 } });
            corridor4.SetExits(new Exits { { Room.Exit.South, garden }, { Room.Exit.East, no1qtrs }, { Room.Exit.West, qtrs1 } });
            garden.SetExits(new Exits { { Room.Exit.North, corridor4 }, { Room.Exit.South, corridor5 }, { Room.Exit.East, corridor3 }, { Room.Exit.West, airlock } });
            corridor5.SetExits(new Exits { { Room.Exit.North, garden }, { Room.Exit.East, capqtrs }, { Room.Exit.West, qtrs2 } });
            no1qtrs.SetExits(new Exits { { Room.Exit.West, corridor4 } });
            capqtrs.SetExits(new Exits { { Room.Exit.West, corridor5 } });
            weapons.SetExits(new Exits { { Room.Exit.East, corridor2 } });
            bridge.SetExits(new Exits { { Room.Exit.East, corridor1 } });
            corridor2.SetExits(new Exits { { Room.Exit.South, corridor1 }, { Room.Exit.East, engineering2 }, { Room.Exit.West, weapons } });
            corridor1.SetExits(new Exits { { Room.Exit.North, corridor2 }, { Room.Exit.South, corridor3 }, { Room.Exit.East, observation }, { Room.Exit.West, bridge } });
            corridor3.SetExits(new Exits { { Room.Exit.North, corridor1 }, { Room.Exit.South, docking }, { Room.Exit.East, engineering1 }, { Room.Exit.West, garden } });
            docking.SetExits(new Exits { { Room.Exit.North, corridor3 } });
            engineering2.SetExits(new Exits { { Room.Exit.South, engineering1 }, { Room.Exit.East, tools }, { Room.Exit.West, corridor2 } });
            observation.SetExits(new Exits { { Room.Exit.East, mess }, { Room.Exit.West, corridor1 } });
            engineering1.SetExits(new Exits { { Room.Exit.North, engineering2 }, { Room.Exit.East, engine1 }, { Room.Exit.West, corridor3 } });
            tools.SetExits(new Exits { { Room.Exit.West, engineering2 } });
            mess.SetExits(new Exits { { Room.Exit.East, galley }, { Room.Exit.West, observation } });
            engine2.SetExits(new Exits { { Room.Exit.South, engine1 } });
            galley.SetExits(new Exits { { Room.Exit.West, mess } });
            engine1.SetExits(new Exits { { Room.Exit.North, engine2 }, { Room.Exit.South, mysterious }, { Room.Exit.West, engineering1 } });
            mysterious.SetExits(new Exits { { Room.Exit.North, engine1 } });
            mysterious.IsTransporter = true;

            shuttle.ExtendedDescription = "A cramped, but comfortable chair sits before a glistening control panel.";
            qtrs1.ExtendedDescription = "Comfortable quarters for a lower-level officer.";
            airlock.ExtendedDescription = "A bare room with metallic silver walls. A large red button above a keypad is on a panel by the shuttle side door. It looks slightly loose.";
            qtrs2.ExtendedDescription = "Quarters for a higher-level officer.";
            corridor4.ExtendedDescription = "A large viewing window looks out of the starboard bulkhead. Various pipes and cables adorn the cieling.";
            corridor5.ExtendedDescription = "A large viewing window looks out of the port bulkhead. Various pipes and cables adorn the cieling. A large plaque is on the aft wall. It reads, \"In honour of Captain: fearless to the last.\"";
            no1qtrs.ExtendedDescription = "The 1st officer's quarters. Very posh. Big fluffy cushions sit on a couch, and a bed covered in fine linen is in the corner.";
            capqtrs.ExtendedDescription = "Gold and silver coloured walls make the room look extremely rich. A conversation pit is in the center, and a bedroom leads off on one side.";
            weapons.ExtendedDescription = "Weapons adorn each wall of this dark room.";
            bridge.ExtendedDescription = "A large circular command station lies in the middle of the room, and various other workstations line the walls.";
            corridor2.ExtendedDescription = "A window provides a good view out the starboard side.";
            corridor1.ExtendedDescription = "A dark passage with doors on each wall.";
            corridor3.ExtendedDescription = "A dark passage, but with light coming from the foreward wall.";
            engineering2.ExtendedDescription = "Tools lay scattered about on benches and collections of parts lie on the floor.";
            engineering1.ExtendedDescription = "A large desk covered in messy food scraps dominates the room. A loud whirr emanates from the aft wall.";
            tools.ExtendedDescription = "A tool locker full of different tools, none of which look particularly familiar to you.";
            mess.ExtendedDescription = "A large room with a number of tables surrounded by chairs. To the aft you can smell food.";
            engine2.ExtendedDescription = "An extremely dark and smelly room with an extremely loud engine in it.";
            galley.ExtendedDescription = "Food preperation benches and stoves surround the room, with a large island in the center.";
            engine1.ExtendedDescription = "A loud engine sits in the center of the room, and makes a loud noise.";
            docking.ExtendedDescription = "It's a small room, and there is not much in it. At one end is a large round airlock, however the glowing red light above suggests to you that there is nothing behind it but space. A door leads off to the north.";
            observation.ExtendedDescription = "It's a long, spacious room. The most striking feature is the huge ceiling, made of some transparent material. Through it, in the distance, you can make out what appears to be a space station.";
            garden.ExtendedDescription = "A huge space lies around you, filled with plants and smelling wonderful. A number of paths wind their way through the planting boxes. Fine water mist is being sprayed in some places.";

            #endregion

            #region Items

            Item spanner, accesscard, fork, bucket, sandwich, terminal, wire, shovel;
            Item seeds, redherring, captainskey, component, plasmagun, compad, box, paper;
            Item controlpanel, airlockpanel, exposedwires, powerdevice;

            powerdevice = new Item("powerdevice", "Power Device", "A power source", "You look it over, and decide that the component would draw energy from the plasma gun. It must be a power source.", 6);

            exposedwires = new Item("exposedwires", "Exposed Wires", "A collection of wires that control the door", "You have no idea which to cross in order to open the door. You need some sort of diagnostic device.", 100);
            exposedwires.PickupMessage = "Pulling out all the wires will not help open the door.";
            exposedwires.CanPickup = false;

            airlockpanel = new Item("airlockpanel", "Airlock Panel", "A panel covering the door wiring", "It looks a bit loose, but you can't move it with your bare hands.", 100);
            airlockpanel.PickupMessage = "You try and try, but cannot move it with your hands.";
            airlockpanel.CanPickup = false;
            airlock.AddItem(airlockpanel);

            controlpanel = new Item("controlpanel", "Shuttle Control Panel", "The shuttle's control panel", "It has lots of buttons and displays, and a small hole into which you might connect something.", 100);
            controlpanel.PickupMessage = "You cannot pick up the shuttle's control panel. It is firmly attached to the shuttle.";
            controlpanel.CanPickup = false;
            shuttle.AddItem(controlpanel);

            compad = new Item("compad", "Computer Pad", "A computer pad", "It is a mobile diagnostic computer pad.", 2);
            tools.AddItem(compad);

            plasmagun = new Item("plasmagun", "Plasmagun", "A plasma weapon", "It looks very ominous. A big red button looks like the trigger. You make sure that there is nothing in your pocket that might press that button.", 4);
            plasmagun.PickupMessage = "You carefully pick up the gun and cautiously place it into your pocket.";
            weapons.AddItem(plasmagun);

            component = new Item("component", "Metallic Component", "A small metallic component", "It's beyond your knowledge what this does, but one part of it looks like it fits something else.", 4);
            engine2.AddItem(component);

            paper = new Item("paper", "Paper", "What seems to be a letter",
                "You pick it up a read it. \"Dear Captain, I've got this nasty person I need you to take care of. Stole some money off me a while back. I need them gone. Yours, Raavis\". Oh my! That's you! So now you know why you're here. You shouldn't steal money... it's gets you nowhere. Not even out of this room.", 1);
            paper.CanPickup = false;
            paper.PickupMessage = "You should leave that alone whilst the Captain is still in the room!";
            paper.IsExaminable = false;
            capqtrs.AddItem(paper);

            box = new Item("box", "Small Box", "A small metal box", "When you pick it up and shake it around, you can hear something rattle inside it.", 2);
            box.CanPickup = false;
            box.PickupMessage = "You should leave that alone whilst the Captain is still in the room!";
            box.IsExaminable = false;
            capqtrs.AddItem(box);

            captainskey = new Item("captainskey", "Captain's Key", "The captain's master key", "It's a plastic card that can fit all the locks on the ship.", 1);

            redherring = new Item("redherring", "Red Herring", "A smelly fish", "You're sure this means something... has some relevance. But you're not sure what.", 5);
            no1qtrs.AddItem(redherring);

            seeds = new Item("seeds", "Seed Bag", "A small bag of green seeds", "The bag is sealed airtight. You notice that the seeds glow slightly in the darkness of your pocket.", 1);
            qtrs1.AddItem(seeds);

            spanner = new Item("spanner", "Spanner", "A smart looking shiny spanner", "It appears never to have been used.", 3);
            docking.AddItem(spanner);

            accesscard = new Item("accesscard", "Access Card", "A plastic access card", "It has a chip in it, and \"Engineering Level 2\" printed on one side.", 1);
            corridor4.AddItem(accesscard);

            fork = new Item("fork", "Fork", "A metal fork", "It's been used recently, and still has bits of food stuck to it. Yuck.", 1);
            mess.AddItem(fork);

            bucket = new Item("bucket", "Bucket", "A small metal pail", "There are no holes in this bucket, but it has a bit of wire attached as a handle.", 5);
            bucket.PickupMessage = "You hang the bucket on a hook attached to your jacket.";
            galley.AddItem(bucket);

            wire = new Item("wire", "Wire", "A piece of wire", "It is a reasonably thick piece of wire.", 1);

            sandwich = new Item("sandwich", "Sandwich", "A clean-cut sandwich", "It has some sort of green leaf in it along with some red meat. Not very appetising.", 1);
            galley.AddItem(sandwich);

            terminal = new Item("terminal", "Computer Terminal", "A computer access terminal", "It uses a touch-screen for input, but seems to require an access card before it can be used.", 100);
            terminal.CanPickup = false;
            terminal.PickupMessage = "It would take too long to cut it out of the bulkhead with your bare hands. It would be dangerous to carry such a heavy weight around anyway.";
            observation.AddItem(terminal);

            shovel = new Item("shovel", "Shovel", "A long-handled shovel", "It's in good condition, and has only a small bit of dirt on it.", 5);
            shovel.PickupMessage = "You insert the shovel between yourself and your belt. You feel somewhat conspicuous.";
            garden.AddItem(shovel);

            #endregion

            #region Npcs

            #region Captain

            Npc captain;

            captain = new Npc("captain", "Captain", "He's wearing his captain's uniform");
            captain.ExtendedDescription = "He looks to be a proud man, or at least a man that used to be proud of something. He has an odd glint in his eye... and you're a bit suspicious of him.";

            //Dialogue captainsDialogue = new Dialogue(captain.ProperName);
            //captainsDialogue.Add("I'm the captain of this ship.");
            //captainsDialogue.Add("Hello.", true);
            //captainsDialogue.Add("What are you doing here?");
            //captainsDialogue.Add("I don't really know.", true);
            //captainsDialogue.Add("Well, be that as it may, you really shouldn't be on the bridge, my friend.");
            //captainsDialogue.Add("Oh I see.", true);
            //captainsDialogue.Add("Why don't you meet me in my quarters? We can discuss your situation.");
            //captainsDialogue.Add("Sure, sounds great.", true);
            //captainsDialogue.Add("Good. Find me just port of the garden.");

            //captain.AddDialogue(captainsDialogue);

            //captainsDialogue = new Dialogue(captain.ProperName);
            //captainsDialogue.Add("Welcome, my friend, to my quarters.");
            //captainsDialogue.Add("Thanks, sir.", true);
            //captainsDialogue.Add("Now, what are you doing on my ship?");
            //captainsDialogue.Add("I can't tell you, I don't know.", true);
            //captainsDialogue.Add("That's not good enough. Do you think I can afford to have just anybody wandering around my ship at will?");
            //captainsDialogue.Add("Well, no, but...", true);
            //captainsDialogue.Add("No buts. You'll just have to stay here until I can determine who you are. Our brig is undergoing repairs at the moment, but this room is extremely secure. I'll return wihtin the hour.");

            //captain.AddDialogue(captainsDialogue);

            //bridge.AddNpc(captain.Name);

            #endregion

            #region Archet

            Npc archet;

            archet = new Npc("archet", "Archet", "She's a tall, dark woman wearing a cadet's uniform.");
            archet.ExtendedDescription = "Her uniform is covered in dirty patches, and her hands are rough. She has a stern look on her face, which suggests that she's not a woman to be messed with.";

            //Dialogue archetDialogue = new Dialogue(archet.ProperName);
            //archetDialogue.Add("Hi, I'm Archet, the ship's gardener.");
            //archetDialogue.Add("You must be new on board. I don't recognise your face.");
            //archetDialogue.Add("No, I've only just come aboard.", true);
            //archetDialogue.Add("Yes. What do you know about gardening?");
            //archetDialogue.Add("Not much, really.", true);
            //archetDialogue.Add("Oh well, but since you're here, I've misplaced some seeds that I need to plant. Could you find them for me?");
            //archetDialogue.Add("Sure, why not.", true);
            //archetDialogue.Add("Great. I'll be right here when you find them.");

            //archet.AddDialogue(archetDialogue);

            //archetDialogue = new Dialogue(archet.ProperName);
            //archetDialogue.Add("Hi, I'm Archet, the ship's gardener.");
            //archetDialogue.Add("You must be new on board. I don't recognise your face.");
            //archetDialogue.Add("No, I've only just come aboard.", true);
            //archetDialogue.Add("Yes. What do you know about gardening?");
            //archetDialogue.Add("Not much, really.", true);
            //archetDialogue.Add("Oh well, but thanks for getting me those seeds, anyway.");
            //archetDialogue.Add("No problems.", true);
            //archetDialogue.Add("See you later.");

            //archet.AddDialogue(archetDialogue);

            //archetDialogue = new Dialogue(archet.ProperName);
            //archetDialogue.Add("Hello again. Have you found the seeds?");
            //archetDialogue.Add("No, I haven't.", true);
            //archetDialogue.Add("Oh well, come back when you have.");
            //archetDialogue.Add("Ok, I will.", true);

            //archet.AddDialogue(archetDialogue);

            //archetDialogue = new Dialogue(archet.ProperName);
            //archetDialogue.Add("Hello again. Have you found the seeds?");
            //archetDialogue.Add("Yes, I have.", true);
            //archetDialogue.Add("Good. Give them to me.");
            //archetDialogue.Add("OK.", true);

            //archet.AddDialogue(archetDialogue);

            //archetDialogue = new Dialogue(archet.ProperName);
            //archetDialogue.Add("Hello.");
            //archetDialogue.Add("How's it going?", true);
            //archetDialogue.Add("Not bad, thanks.");
            //archetDialogue.Add("Nice talking with you.", true);

            //archet.DefaultDialogue = archetDialogue;

            //garden.AddNpc(archet.Name);

            #endregion

            #region Bill

            Npc bill;

            bill = new Npc("bill", "Bill", "He's a rather large Scotsman. He has what seems to be odd red pyjamas on.");
            bill.ExtendedDescription = "You notice small pieces of old food around his mouth, and he smells rather disgusting. You guess a mixture of oil, sweat and garlic.";

            //Dialogue billDialogue = new Dialogue(bill.ProperName);
            //billDialogue.Add("Hello laddie, I'm Bill.");
            //billDialogue.Add("Hello Bill.", true);
            //billDialogue.Add("Oh my, but I'm so very hungry. Ha' ye got ought to eat?");
            //billDialogue.Add("I'm sure I can find something.", true);

            //bill.AddDialogue(billDialogue);

            //billDialogue = new Dialogue(bill.Name);
            //billDialogue.Add("Hello again, laddie.");
            //billDialogue.Add("Hi, Bill.", true);
            //billDialogue.Add("Ach, y'know, I'm still hungry. It's so bad, it isn'e funny.");
            //billDialogue.Add("Oh well, you'll just have to wait until I've found something.", true);

            //bill.AddDialogue(billDialogue);

            //billDialogue = new Dialogue(bill.Name);
            //billDialogue.Add("Hello again, laddie.");
            //billDialogue.Add("Hi, Bill.", true);
            //billDialogue.Add("Ach, y'know, I'm still hungry. It's so bad, it isn'e funny.");
            //billDialogue.Add("As it happens, I think I have something that you might enjoy.", true);

            //bill.AddDialogue(billDialogue);

            //billDialogue = new Dialogue(bill.Name);
            //billDialogue.Add("Hello again, laddie.");
            //billDialogue.Add("Hi, Bill.", true);
            //billDialogue.Add("Ach, thanks for that sandwich. It were very good.");
            //billDialogue.Add("You're welcome, Bill.", true);

            //bill.DefaultDialogue = billDialogue;

            //engineering1.AddNpc(bill.Name);

            #endregion

            #region Pudlin

            Npc pudlin;

            pudlin = new Npc("pudlin", "Pudlin", "An odd looking alien creature");
            pudlin.ExtendedDescription = "He is purple with big green freckles on what seems to be his face. Three large eyeballs stare at you.";

            //Dialogue pudlinDialogue = new Dialogue(pudlin.ProperName);
            //pudlinDialogue.Add("I am Pudlin.");
            //pudlinDialogue.Add("Hello, Pudlin.", true);
            //pudlinDialogue.Add("I'm very busy, so I can't really talk.");
            //pudlinDialogue.Add("OK, then. See you around.", true);

            //pudlin.DefaultDialogue = pudlinDialogue;

            //pudlin.MoveRandom = true;

            //observation.AddNpc(pudlin.Name);

            #endregion

            #endregion

            #region Actions

            // These would be much easier to create with a designer program

            #region Unlock doors with Terminal

            //// use the terminal in observation, with accesscard unlocks three doors
            //Occurrence obsterminal = new Occurrence("obsterminal", Parser.ParserFlags.Use, terminal.Name,
            //    new Action(Action.ActionTypes.Message, "You insert the card into the slot, and the screen turns on. That hacking course finally comes in handy, and you break in to the ship's door system. You find the access code for the doors! Now you can travel through more places in the ship."),
            //    "You need an access card to use the computer.");
            //obsterminal.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, engineering2.Name, true));
            //obsterminal.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, engineering1.Name, true));
            //obsterminal.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, bridge.Name, true));
            //obsterminal.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, accesscard.Name, true));

            #endregion

            #region Various Combines

            //// combine spanner and bucket to get wire
            //Occurrence spannerandbucket = new Occurrence("spannerandbucket", Parser.ParserFlags.Combine, "spanner bucket",
            //    new Action(Action.ActionTypes.Message, "You use the spanner to undo the screws holding the wire to the bucket, and throw away the bucket."));
            //spannerandbucket.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, bucket.Name, false));
            //spannerandbucket.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, wire.Name, true));
            //spannerandbucket.AddAction(new Action(Action.ActionTypes.AddItemToInv, wire.Name, null));
            //spannerandbucket.AddAction(new Action(Action.ActionTypes.RemoveItemFromInv, bucket.Name, null));

            //// combine plasmagun and component to get power source
            //Occurrence plasmagunandcomponent = new Occurrence("plasmagunandcomponent", Parser.ParserFlags.Combine, "plasmagun component",
            //    new Action(Action.ActionTypes.Message, "You fit the end of the plasma gun into the socket on the component. Wow, you're an engineer. You assume it is now a portable power source. Now, you just have to find a use for it."));
            //plasmagunandcomponent.AddAction(new Action(Action.ActionTypes.AddItemToInv, powerdevice.Name, null));
            //plasmagunandcomponent.AddAction(new Action(Action.ActionTypes.RemoveItemFromInv, plasmagun.Name, null));
            //plasmagunandcomponent.AddAction(new Action(Action.ActionTypes.RemoveItemFromInv, component.Name, null));


            #endregion

            //// enter garden whilst carrying shovel
            //Occurrence entergarden = new Occurrence("entergarden", Parser.ParserFlags.GoRoom, garden.Name,
            //    new Action(Action.ActionTypes.Message,
            //    "You stick to the sides of the garden to avoid Archet's gaze. Luckily, she is busy in a far corner."));
            //entergarden.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, shovel.Name, true));

            #region Captain's Dialogue and Quarters and Bridge

            //// talk to captain on the bridge
            //Occurrence talkcaptain = new Occurrence("talkcaptain", Parser.ParserFlags.Talk, captain.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 0));
            //talkcaptain.AddAction(new Action(Action.ActionTypes.MoveNpc, captain.Name, capqtrs.Name));
            //talkcaptain.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, capqtrs.Name, true));
            //talkcaptain.AddAction(new Action(Action.ActionTypes.ChangeMarker, "captainmove1", 1));
            //talkcaptain.AddAction(new Action(Action.ActionTypes.DisableOccurrence, "talkcaptain", null));
            //talkcaptain.AddAction(new Action(Action.ActionTypes.EnableOccurrence, "talkcaptain2", null));
            //talkcaptain.AddCondition(new Condition(Condition.ConditionTypes.Marker, "captainmove1", 0));
            //talkcaptain.AddCondition(new Condition(Condition.ConditionTypes.CurrentRoom, null, bridge.Name));

            //// talk to captain in his quarters
            //Occurrence talkcaptain2 = new Occurrence("talkcaptain2", Parser.ParserFlags.Talk, captain.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 1));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, corridor5.Name, false));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.MoveNpc, captain.Name, bridge.Name));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.DisableOccurrence, "talkcaptain2", null));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.EnableOccurrence, "nofork", null));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeMarker, "captainhaslocked", 1));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeItemPickup, box.Name, true));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeItemPickupMsg, box.Name, ""));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeItemPickup, paper.Name, true));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeItemPickupMsg, paper.Name, ""));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeItemExaminable, box.Name, true));
            //talkcaptain2.AddAction(new Action(Action.ActionTypes.ChangeItemExaminable, paper.Name, true));
            //talkcaptain2.AddCondition(new Condition(Condition.ConditionTypes.CurrentRoom, null, capqtrs.Name));
            //talkcaptain2.AddCondition(new Condition(Condition.ConditionTypes.Marker, "captainmove1", 1));
            //talkcaptain2.AddCondition(new Condition(Condition.ConditionTypes.RoomHasNpc, capqtrs.Name, captain.Name));
            //talkcaptain2.Active = false;

            //// lose if no fork
            //Occurrence nofork = new Occurrence("nofork", Parser.ParserFlags.GoRoom, corridor5.Name,
            //    new Action(Action.ActionTypes.LoseGame, "You try to leave the room, but realise the door is really locked. You search his room ceiling to floor, but find nothing of use. After an hour, you hear the Captain approach his door. If only you had something to open that box!!"));
            //nofork.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, fork.Name, false));
            //nofork.AddCondition(new Condition(Condition.ConditionTypes.Marker, "captainmove1", 1));
            //nofork.AddCondition(new Condition(Condition.ConditionTypes.Marker, "captainhaslocked", 1));
            //nofork.AddCondition(new Condition(Condition.ConditionTypes.RoomHasNpc, bridge.Name, captain.Name));
            //nofork.Active = false;

            //// lose game if you enter the bridge after you've escaped from captains quarters
            //Occurrence enterbridge = new Occurrence("enterbridge", Parser.ParserFlags.GoRoom, bridge.Name,
            //    new Action(Action.ActionTypes.LoseGame, "As you pass through the doors, the captain shouts out \"How did you get out?\" and before you are aware, he pulls out his plasma gun and shoots you. You die instantly. Too bad."));
            //enterbridge.AddCondition(new Condition(Condition.ConditionTypes.Marker, "captainhaslocked", 1));

            //// use the fork on the box to open it to get the captain's key
            //Occurrence forkonbox = new Occurrence("forkonbox", Parser.ParserFlags.Combine, "fork box",
            //    new Action(Action.ActionTypes.Message, "You quickly fashion a slim-jim out of the long pronged fork, and fiddle for a short while with the box's lock. Eventually, it clicks open, and out of it falls a key."),
            //    "You should talk to the captain first before you go rifling through his things.");
            //forkonbox.AddAction(new Action(Action.ActionTypes.AddItemToRoom, captainskey.Name, capqtrs.Name));
            //forkonbox.AddAction(new Action(Action.ActionTypes.RemoveItemFromInv, box.Name, null));
            //forkonbox.AddAction(new Action(Action.ActionTypes.RemoveItemFromRoom, box.Name, capqtrs.Name));
            //forkonbox.AddAction(new Action(Action.ActionTypes.DisableOccurrence, nofork.Name, null));
            //forkonbox.AddCondition(new Condition(Condition.ConditionTypes.RoomHasNpc, bridge.Name, captain.Name));

            #endregion

            #region Archet Dialogues and Giveseeds

            //// not carrying seeds, have not talked previously to archet
            //Occurrence talkarchet = new Occurrence("talkarchet", Parser.ParserFlags.Talk, archet.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 0));
            //talkarchet.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkarchet.Name, null));
            //talkarchet.AddAction(new Action(Action.ActionTypes.DisableOccurrence, "talkarchet1", null));
            //talkarchet.AddAction(new Action(Action.ActionTypes.EnableOccurrence, "talkarchet2", null));
            //talkarchet.AddAction(new Action(Action.ActionTypes.ChangeMarker, "talkedarchet", 1));
            //talkarchet.AddCondition(new Condition(Condition.ConditionTypes.Marker, "talkedarchet", 0));
            //talkarchet.AddCondition(new Condition(Condition.ConditionTypes.Marker, "givenseeds", 0));

            //// already given seeds to archet without talking first
            //Occurrence talkarchet1 = new Occurrence("talkarchet1", Parser.ParserFlags.Talk, archet.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 1));
            //talkarchet1.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkarchet.Name, null));
            //talkarchet1.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkarchet1.Name, null));
            //talkarchet1.AddAction(new Action(Action.ActionTypes.EnableOccurrence, "talkarchet4", null));
            //talkarchet1.AddAction(new Action(Action.ActionTypes.ChangeMarker, "talkedarchet", 1));
            //talkarchet1.AddCondition(new Condition(Condition.ConditionTypes.Marker, "talkedarchet", 0));
            //talkarchet1.AddCondition(new Condition(Condition.ConditionTypes.Marker, "givenseeds", 1));

            //// talked to archet once, now haven't got seeds
            //Occurrence talkarchet2 = new Occurrence("talkarchet2", Parser.ParserFlags.Talk, archet.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 2));
            //talkarchet2.Active = false;
            //talkarchet2.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, seeds.Name, false));

            //// talked to archet once at least, now have seeds
            //Occurrence talkarchet3 = new Occurrence("talkarchet3", Parser.ParserFlags.Talk, archet.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 3));
            //talkarchet3.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, seeds.Name, true));

            //// now have given seeds to archet
            //Occurrence talkarchet4 = new Occurrence("talkarchet4", Parser.ParserFlags.Talk, archet.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 4));
            //talkarchet4.Active = false;
            //talkarchet4.AddCondition(new Condition(Condition.ConditionTypes.Marker, "talkedarchet", 1));

            //// give seeds to archet
            //Occurrence giveseeds = new Occurrence("giveseeds", Parser.ParserFlags.GiveItem, "archet seeds",
            //    new Action(Action.ActionTypes.GiveItemToNpc, archet.Name, seeds.Name, "You hand Archet the seeds and she thanks you. You wonder what you reward for that was, but obviously she doesn't."));
            //giveseeds.AddAction(new Action(Action.ActionTypes.ChangeMarker, "givenseeds", 1));
            //giveseeds.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkarchet2.Name, null));
            //giveseeds.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkarchet3.Name, null));
            //giveseeds.AddAction(new Action(Action.ActionTypes.EnableOccurrence, talkarchet4.Name, null));
            //giveseeds.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, seeds.Name, true));

            #endregion

            #region Bill's dialogue and givesandwich

            //// not carrying sandwich, have not talked previously to bill
            //Occurrence talkbill = new Occurrence("talkbill", Parser.ParserFlags.Talk, bill.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 0));
            //talkbill.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkbill.Name, null));
            //talkbill.AddAction(new Action(Action.ActionTypes.DisableOccurrence, "talkbill1", null));
            //talkbill.AddAction(new Action(Action.ActionTypes.EnableOccurrence, "talkbill2", null));
            //talkbill.AddAction(new Action(Action.ActionTypes.ChangeMarker, "talkedbill", 1));
            //talkbill.AddCondition(new Condition(Condition.ConditionTypes.Marker, "talkedbill", 0));
            //talkbill.AddCondition(new Condition(Condition.ConditionTypes.Marker, "givensandwich", 0));

            //// already given sandwich to bill without talking first
            //Occurrence talkbill1 = new Occurrence("talkbill1", Parser.ParserFlags.Talk, bill.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 3));
            //talkbill1.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkbill.Name, null));
            //talkbill1.AddAction(new Action(Action.ActionTypes.DisableOccurrence, talkbill1.Name, null));
            //talkbill1.AddAction(new Action(Action.ActionTypes.EnableOccurrence, "talkbill4", null));
            //talkbill1.AddAction(new Action(Action.ActionTypes.ChangeMarker, "talkedbill", 1));
            //talkbill1.AddCondition(new Condition(Condition.ConditionTypes.Marker, "talkedbill", 0));
            //talkbill1.AddCondition(new Condition(Condition.ConditionTypes.Marker, "givensandwich", 1));

            //// talked to bill once or more, now haven't got sandwich
            //Occurrence talkbill2 = new Occurrence("talkbill2", Parser.ParserFlags.Talk, bill.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 1));
            //talkbill2.Active = false;
            //talkbill2.AddCondition(new Condition(Condition.ConditionTypes.Marker, "givensandwich", 0));
            //talkbill2.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, sandwich.Name, false));

            //// talked to bill once at least, now have sandwich
            //Occurrence talkbill3 = new Occurrence("talkbill3", Parser.ParserFlags.Talk, bill.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 2));
            //talkbill3.AddCondition(new Condition(Condition.ConditionTypes.Marker, "givensandwich", 0));
            //talkbill3.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, sandwich.Name, true));

            //// now have given sandwich to bill
            //Occurrence talkbill4 = new Occurrence("talkbill4", Parser.ParserFlags.Talk, bill.Name,
            //    new Action(Action.ActionTypes.StartDialogue, null, 3));
            //talkbill4.Active = false;
            //talkbill4.AddCondition(new Condition(Condition.ConditionTypes.Marker, "givensandwich", 1));
            //talkbill4.AddCondition(new Condition(Condition.ConditionTypes.Marker, "talkedbill", 1));

            //// give sandwich to bill
            //Occurrence givesandwich = new Occurrence("givesandwich", Parser.ParserFlags.GiveItem, "bill sandwich",
            //    new Action(Action.ActionTypes.Message, "Bill grabs the sandwich and with a loud moan runs off to consume it. He doesn't notice that he flipped the unlock switch for the engine-room doors."));
            //givesandwich.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, engine1.Name, true));
            //givesandwich.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, engine2.Name, true));
            //givesandwich.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, mysterious.Name, true));
            //givesandwich.AddAction(new Action(Action.ActionTypes.MoveNpc, bill.Name, mess.Name));
            //givesandwich.AddAction(new Action(Action.ActionTypes.GiveItemToNpc, bill.Name, sandwich.Name));
            //givesandwich.AddAction(new Action(Action.ActionTypes.ChangeMarker, "givensandwich", 1));
            //givesandwich.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, sandwich.Name, true));

            #endregion

            #region Captain's Key

            //// give access to rooms when the captain's key is in inventory
            //Occurrence capkey = new Occurrence("capkey", Parser.ParserFlags.PickUpItem, captainskey.Name,
            //    new Action(Action.ActionTypes.ChangeObjectAcc, airlock.Name, true),
            //    "You need the access card to use the Captain's key.");
            //capkey.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, capqtrs.Name, true));
            //capkey.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, qtrs2.Name, true));
            //capkey.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, qtrs1.Name, true));
            //capkey.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, no1qtrs.Name, true));
            //capkey.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, weapons.Name, true));
            //capkey.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, tools.Name, true));
            //capkey.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, corridor5.Name, true));
            //capkey.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, accesscard.Name, true));

            //// disallow access to rooms when the captain's key is dropped
            //Occurrence capkeydrop = new Occurrence("capkeydrop", Parser.ParserFlags.PutDownItem, captainskey.Name,
            //    new Action(Action.ActionTypes.ChangeObjectAcc, airlock.Name, false));
            //capkeydrop.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, capqtrs.Name, false));
            //capkeydrop.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, qtrs2.Name, false));
            //capkeydrop.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, qtrs1.Name, false));
            //capkeydrop.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, no1qtrs.Name, false));
            //capkeydrop.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, weapons.Name, false));
            //capkeydrop.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, tools.Name, false));
            //capkeydrop.AddCondition(new Condition(Condition.ConditionTypes.CarryingItem, accesscard.Name, true));

            #endregion

            #region Get to shuttlecraft

            //// use the shovel on the airlockpanel to open it
            //Occurrence shovelonairlock = new Occurrence("shovelonairlock", Parser.ParserFlags.Combine, shovel.Name + " " + airlockpanel.Name,//"shovel airlockpanel",
            //    new Action(Action.ActionTypes.Message, "You wedge the shovel in between the panel and the bulkhead. As you pull down, you hear cracking sounds from the shovel's handle. You fear the worst, however, in an instant the panel snaps off with a loud crack, and the shovel snaps in two."));
            //shovelonairlock.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, airlockpanel.Name, false));
            //shovelonairlock.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, exposedwires.Name, true));
            //shovelonairlock.AddAction(new Action(Action.ActionTypes.RemoveItemFromInv, shovel.Name, null));
            //shovelonairlock.AddAction(new Action(Action.ActionTypes.AddItemToRoom, exposedwires.Name, airlock.Name));
            //shovelonairlock.AddAction(new Action(Action.ActionTypes.RemoveItemFromRoom, airlockpanel.Name, airlock.Name));

            //// use wire on exposedwires to unlock the shuttlecraft, after having located correct wires with compad
            //Occurrence wireonexposed = new Occurrence("wireonexposed", Parser.ParserFlags.Combine, "wire exposedwires",
            //    new Action(Action.ActionTypes.Message, "You fit the wire over the appropriate places, and hear a pleasing click emanate from the door."),
            //    "You don't know where to put the wire. You need to find out. Perhaps a diagnostic device would shed some light on the situation.");
            //wireonexposed.AddAction(new Action(Action.ActionTypes.RemoveItemFromInv, wire.Name, null));
            //wireonexposed.AddAction(new Action(Action.ActionTypes.ChangeObjectAcc, shuttle.Name, true));
            //wireonexposed.AddAction(new Action(Action.ActionTypes.RemoveItemFromRoom, exposedwires.Name, airlock.Name));
            //wireonexposed.AddCondition(new Condition(Condition.ConditionTypes.Marker, "hasfoundwire", 1));

            //// use compad on exposedwires to find which to cross
            //Occurrence compadonexposed = new Occurrence("compadonexposed", Parser.ParserFlags.Combine, "compad exposedwires",
            //    new Action(Action.ActionTypes.Message, "You hold the compad up to the exposed wires and press a few buttons. It makes some beeping noises, then shows a schematic diagram of the locking mechanism. You eventually locate the appropriate wires to cross in order to release the lock."));
            //compadonexposed.AddAction(new Action(Action.ActionTypes.ChangeMarker, "hasfoundwire", 1));

            //// use powerdevice on controlpanel to win game
            //Occurrence poweronpanel = new Occurrence("poweronpanel", Parser.ParserFlags.Combine, "powerdevice controlpanel",
            //    new Action(Action.ActionTypes.WinGame, "You plug the power source into the shuttle's control panel and press some buttons. The door behind you closes, and the shuttle departs. You fly it towards the space station in the distance. Well done."));

            #endregion

            #endregion

            GameState gs = new GameState();

            #region Collection Additions

            gs.AddRoom(shuttle);
            gs.AddRoom(qtrs1);
            gs.AddRoom(airlock);
            gs.AddRoom(qtrs2);
            gs.AddRoom(corridor4);
            gs.AddRoom(garden);
            gs.AddRoom(corridor5);
            gs.AddRoom(no1qtrs);
            gs.AddRoom(capqtrs);
            gs.AddRoom(weapons);
            gs.AddRoom(bridge);
            gs.AddRoom(corridor2);
            gs.AddRoom(corridor1);
            gs.AddRoom(corridor3);
            gs.AddRoom(docking);
            gs.AddRoom(engineering2);
            gs.AddRoom(observation);
            gs.AddRoom(engineering1);
            gs.AddRoom(tools);
            gs.AddRoom(mess);
            gs.AddRoom(engine2);
            gs.AddRoom(galley);
            gs.AddRoom(engine1);
            gs.AddRoom(mysterious);

            gs.AddItem(spanner);
            gs.AddItem(accesscard);
            gs.AddItem(fork);
            gs.AddItem(bucket);
            gs.AddItem(sandwich);
            gs.AddItem(terminal);
            gs.AddItem(wire);
            gs.AddItem(shovel);
            gs.AddItem(seeds);
            gs.AddItem(redherring);
            gs.AddItem(captainskey);
            gs.AddItem(compad);
            gs.AddItem(plasmagun);
            gs.AddItem(controlpanel);
            gs.AddItem(airlockpanel);
            gs.AddItem(exposedwires);
            gs.AddItem(powerdevice);
            gs.AddItem(component);
            gs.AddItem(box);
            gs.AddItem(paper);

            gs.AddNpc(captain);
            gs.AddNpc(archet);
            gs.AddNpc(bill);
            gs.AddNpc(pudlin);

            //occurrences.Add(obsterminal.Name, obsterminal);
            //occurrences.Add(spannerandbucket.Name, spannerandbucket);
            //occurrences.Add(entergarden.Name, entergarden);
            //occurrences.Add(talkarchet.Name, talkarchet);
            //occurrences.Add(talkarchet1.Name, talkarchet1);
            //occurrences.Add(talkarchet2.Name, talkarchet2);
            //occurrences.Add(talkarchet3.Name, talkarchet3);
            //occurrences.Add(talkarchet4.Name, talkarchet4);
            //occurrences.Add(giveseeds.Name, giveseeds);
            //occurrences.Add(talkbill.Name, talkbill);
            //occurrences.Add(talkbill1.Name, talkbill1);
            //occurrences.Add(talkbill2.Name, talkbill2);
            //occurrences.Add(talkbill3.Name, talkbill3);
            //occurrences.Add(talkbill4.Name, talkbill4);
            //occurrences.Add(givesandwich.Name, givesandwich);
            //occurrences.Add(capkey.Name, capkey);
            //occurrences.Add(capkeydrop.Name, capkeydrop);
            //occurrences.Add(shovelonairlock.Name, shovelonairlock);
            //occurrences.Add(plasmagunandcomponent.Name, plasmagunandcomponent);
            //occurrences.Add(wireonexposed.Name, wireonexposed);
            //occurrences.Add(compadonexposed.Name, compadonexposed);
            //occurrences.Add(poweronpanel.Name, poweronpanel);
            //occurrences.Add(talkcaptain2.Name, talkcaptain2);
            //occurrences.Add(talkcaptain.Name, talkcaptain);
            //occurrences.Add(enterbridge.Name, enterbridge);
            //occurrences.Add(forkonbox.Name, forkonbox);
            //occurrences.Add(nofork.Name, nofork);

            gs.Variables.Set("givenseeds", false);
            gs.Variables.Set("talkedarchet", false);
            gs.Variables.Set("givensandwich", false);
            gs.Variables.Set("talkedbill", false);
            gs.Variables.Set("captainmove1", false);
            gs.Variables.Set("captainhaslocked", false);
            gs.Variables.Set("hasfoundwire", false);

            #endregion

            //GameInfo gameData = new GameInfo(rooms, items, npcs, occurrences, markers);
            gs.SetWelcomeMessage("Hello, and welcome to OddShip, a short adventure aboard a space craft set in the distant future.");
            //gameData.HelpMessage = "You find yourself aboard what seems to be a space craft. You can't remember how you arrived here, nor why. And your head hurts. You think you should probably find a way off as soon as possible.";
            //gameData.StartRoomKey = "docking";

            gs.SetEgo(new Ego("You", "It's just you."));
            gs.SetCurrentLocation(docking);

            gs.FinaliseSetup();

            return gs;
        }
#endif
    }
}
