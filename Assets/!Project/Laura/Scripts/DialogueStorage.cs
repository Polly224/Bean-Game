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
    
    void Awake()
    {
        if (instance != null) Destroy(gameObject);
        else instance = this;
    }
    public Conversation GetDialogue(string dialogue)
    {
        switch (dialogue)
        {
            case "introdialogue":
                return new Conversation("Narration", new string[] {"[Ah, what a beautiful day... you're feeling quite hungry. Specifically for a can of beans from the store.]", "[They only have 1 can of beans left, you know this. Good luck.]"});
            case "stepoutside":
                return new Conversation("Narration", new string[] {"[...Oh, right. The world hates you.]"});
            case "isaac":
                return new Conversation("Rogue Man", new string[] {"My wife just handed me the divorce papers... She's taking the kids, too.", "...At least my Isaac run is saved!"});
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
                return new Conversation("Strongest Fighter", new string[] { "Hey, it's me! Strongest Fighter!", "Hey, you look pretty strong! I'd love to fight you sometime!", "Seems you're already busy on some kinda adventure, though."});
            case "gokubeans":
                return new Conversation("Strongest Fighter", new string[] {"[You pull out the beans, eagerly handing them to the Strongest Fighter.]", "Oh hey, you're giving me these? Thanks, guy!", "It's a shame I never got to fight you... at least I got a nice snack, though!"});
            case "bug":
                return new Conversation("Bug", new string[] { "...", "[There's a bug here.]", "[You can't see it with your pixelated eyes, but trust me on this one.]"});
            case "bugbeans":
                return new Conversation("Bug", new string[] { "...", "[There's still a bug here.]", "[It takes the beans you hand to it, eagerly eating them.]", "...!", "[It happily waves at you.]", "[You wave back.]" });
            case "hollowknight":
                return new Conversation("Bug", new string[] { "...", "[There's a bug here.]", "You can't see it with your pixelated eyes, but it seems to be holding a nail of some sorts." });
            case "hollowknightbeans":
                return new Conversation("Bug", new string[] { "...", "[There's still a bug here.]", "[It impales one of the beans you hand to it with its nail, eating off it like a skewer.]", "[...Is it eating with its eyes?]", "[Oh, well. It seems to be enjoying it.]" });
            case "caseohsign":
                return new Conversation("Sign", new string[] { "[It's an old, worn down sign. It seems like it's been here for a while.]", "[There's some poorly written text on it.", "\"Sorry to all C*seoh fans, we ran out of budget to expand the room further. We weren't able to fit him in.\"", "...", "[This room is bigger than your entire house.]" });
            case "caseohsignbeans":
                return new Conversation("Sign", new string[] { "[You lay down the can of beans next to the sign.]", "[The sign turns to face it, absorbing the can into its face before the can instantly disappears.]", "...", "[The text on the sign changed.]", "\"Please stop feeding him. You're only making things worse.\"" });
            case "ratcoat":
                return new Conversation("???", new string[] { "[It's a bunch of rats in a trenchcoat, pretending to be a person.]", "...No we're not." });
            case "ratcoatalt":
                return new Conversation("?????", new string[] { "[You're not sure what this is.]", "[You're pretty sure it isn't a bunch of rats in a trenchcoat, though.]" });
            case "ratcoatbeans":
                return new Conversation("???", new string[] {"[You place the can of beans next to the coated figure, which quickly picks it up.]", "[A few moments later, out pops an empty can of beans.]", "[The rats in the coat seem happy.]", "...No, we don't."});
            case "ratcoataltbeans":
                return new Conversation("?????", new string[] { "[You place the can of beans next to the coated figure, which quickly picks it up.]", "[A few moments later, out pops an empty can of beans.]", "[Whatever this coat is filled with, it seems happy.]", "...I guess we are." });
            case "cat":
                return new Conversation("Dog", new string[] { "[It's a dog.]" });
            case "catbeans":
                return new Conversation("Dog", new string[] {"[Dogs can't eat uncooked beans.]", "...*clink!*", "[Well, wouldn't you know it. The Dog knocked over the opened can. What a waste.]"});
            case "longspeech":
                return new Conversation("Wise Old Man", new string[] { "Oh... It’s you.", "I’m… honestly surprised you even managed to find me.", "Not many people visit this alley anymore, you know.", "I must say, you’re the first visitor I’ve had in many years. How fascinating.", "There is… one thing, that I wish to tell you about.", "You see, there’s been something plaguing me for many years now. Something I haven’t been able to talk about to anyone.", "Now that YOU’RE here, though… I suppose I should… ‘spill the beans’, so to speak.", "Not that you really have a say in this. I explicitly told them not to put ‘yes’ or ‘no’ options in my conversation.", "…Anyway. Allow me to tell you a story.", "You see, I used to have this dear friend of mine… his name was Ellivro.", "He always wore a very fancy sombrero, it was his signature hat.", "One day, he leaned over while we were feeding the ducks, you see.", "I said to him, y’know, I leaned over to him, and I said…", "’Ellivro. You gotta stop feeding the ducks, man. I-It’s… it’s not good for 'em. The bread, I mean. They can’t really, like, digest it.’", "…He just stared at me for a while, seemingly unresponsive.", "’…Please wake up.’, he replied.", "…", "Why… why did he say that?" });
            case "longspeechbeans":
                return new Conversation("Wise Old Man", new string[] {"Nice.", "...", "[You're both glad you didn't have to sit through another speech, but also a bit disappointed.]", "[At least he seems to be enjoying his meal.]"});
            case "cigarettepile":
                return new Conversation("Cigarette Pile", new string[] { "[Five... hundred... \n ...Wait, no, this is 499 cigarettes, not 500. You might want to order some more.]"});
            case "cigarettepilebeans":
                return new Conversation("Cigarette Pile", new string[] { "[You pour the beans onto the pile.]", "[Congratulations, you've wasted some perfectly good beans, and cured some random guy's nicotine addiction.]"});
            case "garcello":
                return new Conversation("Green-haired Smoker", new string[] {"[There's a man here, smoking a cigarette.]", "Oh, hey, little guy. Didn't see ya there.", "...You want a smoke? I've got plenty piled up here."});
            case "garcellobeans":
                return new Conversation("Green-haired Smoker", new string[] { "Oh, hey, little guy. What's that you're holding?", "[You hand over the can of beans.]", "AGRHAAGARAGJ" });
            case "beansobtained":
                return new Conversation("Narration", new string[] { "[You've obtained the last can of beans!]", "[You can either go home and eat it yourself, or give it to whoever you want.]", "[Mind you, this means that the next thing you talk to will be given the beans. So choose wisely.]", "[In your hands is the last can of beans.]"});
            case "beansgiven":
                return new Conversation("Narration", new string[] {"[You've given away the last can of beans in existence.]", "...", "Go home, your task is done." });
            case "playerbeans":
                return new Conversation("Narration", new string[] { "[You sit down to eat your delicious can of beans]", "[You tear off the lid with your bare hands, chugging it backwards like it's a glass of beer.]", "...!", "[You don't... feel so good...]", "..."});
            default:
                return new Conversation("error", new string[] { "Whoops, there's no dialogue here.", "Might want to make sure you're not inputting the wrong id."});
    }
    }
}
