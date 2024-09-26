using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;
using static UnityEngine.InputManagerEntry;
using static UnityEngine.ParticleSystem;
using static UnityEngine.Rendering.DebugUI.Table;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using UnityEngine.XR;
using UnityEditor.SceneManagement;

public class DialogueStorage : MonoBehaviour
{
    public static DialogueStorage instance;
    public struct Conversation
    {
        public string name;
        public string[] conversation;

        public Conversation(string name, string[] conversation)
        {
            this.name = name;
            this.conversation = conversation;
        }
    }
    public int cookieClickAmount;
    
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
        cookieClickAmount = 2;
    }
    public Conversation GetDialogue(string dialogue)
    {
        switch (dialogue)
        {
            case "introdialogue":
                return new Conversation("Narration", new string[] {"[Ah, what a beautiful day...]", "[You're spending your day the same way you spend most.]", "[You're standing in the middle of the room, due to you lacking a sitting sprite.]", "...", "[You know, you do feel a bit hungry. Specifically, for an entire can of beans.]", "[...Good lord, you could absolutely devour some beans right now.]", "[There's only one can of beans left at the store. Go and get it.]", "[...And be a kind neighbor. There's lots of people willing to have a chat!]"});
            case "introdialoguealt":
                return new Conversation("Narration", new string[] { "[Ah, what a beautiful day...]", "[You're spending your day the same way you spend most.]", "[You're standing in the middle of the room, due to you lacking a sitting sprite.]", "...", "[Well... sadly, there are no more cans of beans in existence.]", "[That one in the store was the last one, after all.]", "[You have no reason to leave your house anymore.]", "..." });
            case "introdialoguedead":
                return new Conversation("Narration", new string[] { "[Ah, what a beautiful day...]", "[You're...]", "[...........]", "[...Right. You died. What a shame.]", "[Who would have expected you to have a... fatal bean allergy.]", "[Perhaps if you hadn't been oh-so-selfish... if you'd shared your beans with others.]", "[Perhaps you wouldn't have suffered this same fate.]", "[For now, though...]", "[Good night.]"});
            case "stepoutside":
                return new Conversation("Narration", new string[] {"[...Oh, right. The world hates you.]", "...", "[...Well, you can't afford an umbrella, so too bad.]", "[Have fun getting drenched in rainwater on the way there.]"});
            case "cardboardbox":
                return new Conversation("Cardboard Box", new string[] { "[It's just a cardboard box.]", "[Considering the housing prices of nowadays, you had to settle for this.]", "[...You should get those beans first, before you go back inside.]" });
            case "isaac":
                return new Conversation("Rogue Man", new string[] {"My wife just handed me the divorce papers... She's taking the kids, too.", "...At least my Isaac run's looking much better now!", "[The Man is now crying horizontally.]"});
            case "isaacbeans":
                return new Conversation("Divorced Man", new string[] {"[The Divorced Man obtained The Bean.]", "...You're giving me The Bean? My run's great already, I don't need another quality 0. But thanks, I guess." });
            case "kris":
                return new Conversation("Blue-skinned Kid", new string[] {"..."});
            case "krisbeans":
                return new Conversation("Blue-skinned Kid", new string[] {"[You hand over the can of beans to the quiet blue kid.]", "...!", "[He seems happy.]"});
            case "susie":
                return new Conversation("Purple Dragon", new string[] {"Where the FUCK are we?!"});
            case "susiebeans":
                return new Conversation("Purple Dragon", new string[] {"[You anxiously hand over the can of beans to the Purple Dragon.]", "Huh? You want to give me a can of beans?", "...", "HOW THE HELL IS THAT GONNA HELP US FIGURE OUT WHERE WE ARE?!"});
            case "goku":
                return new Conversation("Strongest Fighter", new string[] { "Hey, it's me! The Strongest Fighter!", "Hey, you look pretty strong! I'd love to fight you sometime!", "Seems you're already busy on some kinda adventure, though. Let me know if you're up for a match!"});
            case "gokubeans":
                return new Conversation("Strongest Fighter", new string[] {"[You pull out the beans, eagerly handing them to The Strongest Fighter.]", "Oh hey, you're giving me these? Thanks, guy!", "It's a shame I never got to fight you... at least I got a nice snack, though!", "[Something tells you he's disappointed in the lack of healing the beans are giving him.]"});
            case "bug":
                return new Conversation("Bug", new string[] { "...", "[There's a bug here.]", "[You can barely see it with your pixelated eyes, but trust me on this one.]"});
            case "bugbeans":
                return new Conversation("Bug", new string[] { "...", "[There's still a bug here.]", "[It takes the beans you hand to it, eagerly eating them.]", "...!", "[It happily waves at you. You wave back.]" });
            case "hollowknight":
                return new Conversation("Bug", new string[] { "...", "[There's a bug here.]", "[You can barely see it with your pixelated eyes, but it seems to be holding a nail of some sorts.]", "["});
            case "hollowknightbeans":
                return new Conversation("Bug", new string[] { "...", "[There's still a bug here.]", "[It impales one of the beans you hand to it with its nail, eating off it like a skewer.]", "[...Is it eating with its eyes?]", "[Oh, well. It seems to be enjoying it.]" });
            case "caseohsign":
                return new Conversation("Sign", new string[] { "[It's an old, worn down sign. It seems like it's been here for a while.]", "[There's some poorly written text on it.", "\"Sorry to all C*seoh fans, we ran out of budget to expand the room further. We weren't able to fit him in.\"", "...", "[This room is bigger than your entire house.]" });
            case "caseohsignbeans":
                return new Conversation("Sign", new string[] { "[You lay down the can of beans next to the sign.]", "[The sign turns to face it, absorbing the can into its face before the can instantly disappears.]", "...", "[The text on the sign changed.]", "\"Please stop feeding him. You're only making things worse.\"" });
            case "cookie":
                return new Conversation("Pile of Cookies", new string[] {"[There's " + cookieClickAmount.ToString() + " cookies laying on the ground here.]", "[You reach out and tap your finger on one of the cookies.]", "[...Surprisingly, another cookie pops out and gets added to the pile!]"});
            case "cookiebeans":
                return new Conversation("Pile of Cookies", new string[] {"[There's " + cookieClickAmount.ToString() + " cookies laying on the ground here.]", "[You reach out and pour the beans onto the pile.]", "[...Now it's " + cookieClickAmount.ToString() + " cookies and about 1735 beans.]", "[You've gained quite the collection, it seems.]"});
            case "ratcoat":
                return new Conversation("???", new string[] { "[It's a bunch of rats in a trenchcoat, pretending to be a person.]", "...No we're not." });
            case "ratcoatalt":
                return new Conversation("?????", new string[] { "[You're not sure what this is.]", "[You're pretty sure it isn't a bunch of rats in a trenchcoat, though.]" });
            case "ratcoatbeans":
                return new Conversation("???", new string[] {"[You place the can of beans next to the coated figure, which quickly picks it up.]", "[A few moments later, out pops an empty can of beans.]", "[The rats in the coat seem happy.]", "...No, we don't."});
            case "ratcoataltbeans":
                return new Conversation("?????", new string[] { "[You place the can of beans next to the coated figure, which quickly picks it up.]", "[A few moments later, out pops an empty can of beans.]", "[Whatever this coat is filled with, it seems happy.]", "...I guess we are." });
            case "cat":
                return new Conversation("Dog", new string[] {"[It's a dog.]"});
            case "catbeans":
                return new Conversation("Dog", new string[] {"[Dogs can't eat uncooked beans.]", "...*clink!*", "[Well, wouldn't you know it. The Dog knocked over the opened can. What a waste.]"});
            case "longspeech":
                return new Conversation("Wise Old Hippo", new string[] { "Oh... It's you.", "I'm… honestly surprised you even managed to find me.", "Not many people visit this alley anymore, you know.", "I must say, you’re the first visitor I've had in many years. How fascinating.", "There is… one thing, that I wish to tell you about.", "You see, there's been something plaguing me for many years now. Something I haven’t been able to talk about to anyone.", "Now that YOU’RE here, though… I suppose I should… ‘spill the beans’, so to speak.", "Not that you really have a say in this. I explicitly told them not to put ‘yes’ or ‘no’ options in my conversation.", "…Anyway. Allow me to tell you a story.", "You see, I used to have this dear friend of mine… his name was Ellivro.", "He always wore a very fancy sombrero, it was his signature hat.", "One day, he leaned over while we were feeding the ducks, you see.", "I said to him, y’know, I leaned over to him, and I said…", "’Ellivro. You gotta stop feeding the ducks, man. I-It’s… it’s not good for 'em. The bread, I mean. They can’t really, like, digest it.’", "…He just stared at me for a while, seemingly unresponsive.", "’…Please wake up.’, he replied.", "…", "Why… why did he say that?" });
            case "longspeechbeans":
                return new Conversation("Wise Old Hippo", new string[] {"Thank you, my friend.", "...", "[You're both glad you didn't have to sit through another speech, but also a bit disappointed.]", "[At least he seems to be enjoying his meal.]"});
            case "cigarettepile":
                return new Conversation("Cigarette Pile", new string[] { "[Five... hundred... \n ...Wait, no, this is 499 cigarettes, not 500. You might want to order some more.]"});
            case "cigarettepilebeans":
                return new Conversation("Cigarette Pile", new string[] { "[You pour the beans onto the pile.]", "[Congratulations, you've wasted some perfectly good beans, and cured some random guy's nicotine addiction.]"});
            case "garcello":
                return new Conversation("Green-haired Smoker", new string[] {"[There's a man here, smoking a cigarette.]", "Oh, hey, little guy. Didn't see ya there.", "...You want a smoke? I've got plenty piled up here.", "[You do not want a smoke. You want beans.]"});
            case "garcellobeans":
                return new Conversation("Green-haired Smoker", new string[] { "Oh, hey, little guy. What's that you're holding?", "[You hand over the can of beans.]", "AGRHAAGARAGJ" });
            case "cs:go":
                return new Conversation("Guy Banging On Door", new string[] { "[There's a man on the other side of the door, wearing a balaclava.]", "DOOR STUCK!! DOOR STUCK!!!! PLEASE!!!", "[You don't have a jar of door unstucking with you.]" });
            case "cs:gobeans":
                return new Conversation("Guy Banging On Door", new string[] {"[You give the beans to the balaclava-wearing man.]", "[The banging stops for a moment.]", "...", "[The banging continues again.]", "[That was a can of beans, not a jar of door unstucking.]"});
            case "sona!polly":
                return new Conversation("Tiny Sona", new string[] {"[There's a tiny girl here, wearing a purple hoodie.]", "H-hi!! Hi there!!", "[She reaches her tiny hand out for a high five. You tap it with your finger.]", "[She looks happy. Nice little interaction for the two of you.]" });
            case "sona!pollybeans":
                return new Conversation("Tiny Sona", new string[] { "[You give the tiny girl a bean, and she quickly devours it.]", "[She scatters away, muttering something about cooking. Seems like she didn't like it much.]", "[Maybe bring a can of strawberries, next time?]" });
            case "v1":
                return new Conversation("Bloodthirsty Robot", new string[] { "[There's a winged robot here, holding a gun in one hand and a can of coins in the other.]", "[Seems like the robot's begging for money has resulted in quite a lot of coins in its can. It makes a beeping noise as you walk past.]" });
            case "v1beans":
                return new Conversation("Bloodthirsty Robot", new string[] { "[You pour the beans from your can into the robot's.], [It picks one out, throws it in the air, and shoots it with its revolver.]", "[The bullet pierces right through the bean. \n For whatever reason, it looks disappointed.]"});
            case "gaster":
                return new Conversation("W.D. Gaster", new string[] { "..." });
            case "gasterbeans":
                return new Conversation("W.Bean Gaster", new string[] { "...!" });
            case "screamingorks":
                return new Conversation("Two Orks", new string[] {"[There's two orks standing inches away from each other, screaming and yelling at each other.]", "[They're... really going at it.]", "...", "[Might want to leave them to it. You're not smart enough to follow the discussion.]"});
            case "screamingorksbeans":
                return new Conversation("Two Orks", new string[] { "[You cautiously put down the can of beans in front of the two orks.]", "[They stop screaming for just a moment, looking at the can.]", "[...And just a moment later, they're back to screaming.]", "[You don't feel confident enough you could beat them to pick your beans back up.]" });
            case "purpleork":
                return new Conversation("Purple Ork...?", new string[] { "[There's a... purple ork, sitting in the corner- oh.]", "[Nevermind. You suppose there was nothing there after all. Or maybe there is, you have no idea of knowing.]", "[Purple is a very sneaky color, after all.]" });
            case "purpleorkalt":
                return new Conversation("", new string[] { "[You don't see anything here, so there's probably nothing here.]" });
            case "purpleorkbeans":
                return new Conversation("Purple Ork...?", new string[] { "[There seems to be a purple ork, sitting in the corner- oh.]", "[Just as you gave him the beans, he suddenly disappeared, taking the beans with him.]", "[...You wonder. How would purple beans taste?]" });
            case "purpleorkaltbeans":
                return new Conversation("", new string[] { "[You put the beans down in front of the corner of nothingness.]", "...", "[Suddenly, the beans turn purple, disappearing.]", "[This saddens you. Everyone knows purple things are unfindable, they're very sneaky.]"});
            case "springtrap":
                return new Conversation("Decrepit Suit", new string[] {"[There's an animatronic suit of a bunny, very rugged and old.]", "[You're sure the owner will come back to pick it up, though. He always comes back.]"});
            case "springtrapbeans":
                return new Conversation("Decrepit Suit", new string[] {"[You put the can of beans in the suit's mouth.]", "[Surprisingly, the suit bites down, crushing the can and eating it fully.]", "[You suppose he didn't need to come back in the first place. He never left.]"});
            case "pennywise":
                return new Conversation("Balloon", new string[] { "[It's a floating red balloon. Its string seems to be tied around a sewer grate.]", "[There's a little note with writing and a drawing of a clown attached to the balloon.]", "\"We all float down here.\"", "...", "[This feels like a reference you don't get.]"});
            case "pennywisebeans":
                return new Conversation("Balloon", new string[] { "[You throw the can of beans down the sewer the balloon is attached to.]", "...", "[A moment of silence passes as the clanging of the can echoes down the sewers.]", "[Right as you're about to regret your decision, you hear a creepy laughter coming from inside.]", "[...You suppose someone appreciated your gift.]" });
            case "beansobtained":
                return new Conversation("Narration", new string[] { "[You've obtained the last can of beans!]", "[You can either go home and eat it yourself, or give it to whoever you want.]", "[Mind you, this means that the next thing you talk to will be given the beans. So choose wisely.]", "[In your hands is the last can of beans.]"});
            case "beansgiven":
                return new Conversation("Narration", new string[] {"[You've given away the last can of beans in existence.]", "...", "[Go home, your task is done.]" });
            case "playerbeans":
                return new Conversation("Narration", new string[] { "[You walk into the middle of the room to eat your delicious can of beans.]", "[You tear off the lid with your bare hands, chugging it backwards like it's a glass of beer.]", "...!", "[You don't... feel so good...]", "..."});
            case "playernobeans":
                return new Conversation("Narration", new string[] { "[You enter your home, beans given away, hunger unsatiated.]", "[However... you feel like you did the right thing.]", "[Rather than being egotistical, keeping such a treasure for yourself... you shared it with a stranger.]", "[How nice of you. You did very well.]", "[Thank you. You fulfilled your role wonderfully.]", "[You can rest now. Good night... Sans Undertale.]" });
            default:
                return new Conversation("error", new string[] { "Whoops, there's no dialogue here.", "Might want to make sure you're not inputting the wrong id."});
    }
    }
}
