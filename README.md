An Inscryption mod that lets you easily add animated portraits to your cards with a spritesheet and a JSON file (or, alternatively, through C#).

This is a project done purely for fun. Please don't expect much out of it and please don't get mad if there are bugs.

This mod is currently in its **beta** stage. Please know there may be bugs. If you find any bugs, please contact me on Discord: `kelly betty#7936`

And here's an example of animated cards made with this mod (all art by me):

![Animated Card Example](https://i.imgur.com/47IrttN.gif)

Below you'll find all the **documentation** for this mod, as well as a short **blink animation tutorial**.

# Installation
This mod’s only dependencies are BepInEx and the InscryptionAPI mod.

There are two ways of installing this mod: with the help of a mod manager (like r2modman or the Thunderstore Mod Manager) or manually.

#### Installation (Mod Manager)
1. Download and install [r2modman](https://thunderstore.io/package/ebkr/r2modman/) or the [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager).
2. Install this mod and all of its dependencies with the help of the mod manager! 

#### Installation (Manual)
1. Download and install BepInEx.
    1. If you're downloading it from [its Github page](https://github.com/BepInEx/BepInEx/releases), follow [this installation guide](https://docs.bepinex.dev/articles/user_guide/installation/index.html#where-to-download-bepinex).
    2. If you're downloading ["BepInExPack Inscryption" from Thunderstore](https://inscryption.thunderstore.io/package/BepInEx/BepInExPack_Inscryption/), follow the manual installation guide on the Thunderstore page itself. This one comes with a preconfigured `BepInEx.cfg` file, so it's advised.
3. Download and install the [Inscryption API mod](https://inscryption.thunderstore.io/package/API_dev/API/) following its manual installation guide.
4. Find the `BepInEx > plugins` folder.
5. Place the contents of **"SpritesheetAPI.zip"** in a new folder within the plugins folder.


# Overview
In order to use this mod, you're gonna need two things: A spritesheet, and a way to 'register' that spritesheet in-game.
A JSON file is the easiest way to do it, but C# is also an available option for those who prefer it.

I will go through each of those options in depth below this.

#### JSON
Your JSON file's name should end in **_anim.json** in order for this mod to find it. **Yes, this is necessary.**

And these are all the fields your JSON file should contain:

```json
{
  "CardName": "Kels_Example",
  "Spritesheet": "Example_Spritesheet.png",
  "FrameRate": 12,
  "FrameCount": 4,
  "HasPause": false,
  "PauseTime": 1
}
```

The first four fields—**CardName, Spritesheet, FrameRate and FrameCount**—are *required.* The last two fields—**HasPause and PauseTime**—are optional.

I'll give a thorough explanation of each field later down this page.

#### C#
You can use C# to register a spritesheet directly from your mod's code, like this:

```csharp
RegisterPortrait.Add(CardName: "Kels_Example", Spritesheet: "Example_Spritesheet.png", FrameRate: 12, FrameCount: 4, HasPause: false, PauseTime: 1f);
```

You should preferably do this inside of Awake().

The first four arguments—**CardName, Spritesheet, FrameRate and FrameCount**—are *required*, and the last two arguments—**HasPause and PauseTime**—are optional, and default to false and 0f, respectively.


# Documentation

## Card Name
The in-game name of the card your animated portrait will be attached to. Your mod's GUID should be included in the name.

A few important thing to note about cards:
1. If your card uses an alt portrait for anything, this *might* break the animation.
2. Cards with changing portraits, like the Mud Turtle, *prooooobably* have weird interactions with this mod. I have not tested this. I'm too burnt out to test every possibility.
3. I have not tested how cards affected by this mod interact with the Mycologist. For this reason, it's **heavily** advised you set cards with animated portraits as *'one per deck'* (like how the base game's talking cards are). I was going to test this, but I'm kinda burnt out and don't have the energy.

## The Spritesheet
The most important part of this mod is the spritesheet.

Your spritesheet should follow a few rules:

1. Each frame should be the size of a traditional Inscryption card portrait: 114 x 94 pixels.
2. Your spritesheet should preferably be a .png with transparency.
3. The spritesheet should **not** have any 'padding' between sprites: This *will* break how the mod reads them.

This mod doesn't support colorful portraits by default, but it's fully compatible with mods that do add colorful portraits, such as [SpecialAPI's ColoredPortraits](https://inscryption.thunderstore.io/package/SpecialAPI/ColoredPortraits/). 
You can, thus, use that to make colorful animated portraits—or even just convert random GIFs to card portraits if you want.

A typical spritesheet should look something like this:

![Spritesheet Example](https://i.imgur.com/RhbVjAc.png)

**Note:** I have added the white background **just** so that this image is visible for users using Dark Mode and/or opening this image on a dark background. Your spritesheet's background should be **transparent**.

#### "How do I make a spritesheet?"
For starters: You don't have to put one together by hand! c:

There are many tools you can use to make spritesheets! I'm going to list two of those tools I know.

I use Krita, so the tool I use to make spritesheets is a really neat spritesheet extension called "kritaSpritesheetExporter"! c:

[kritaSpritesheetExporter on Github](https://github.com/Falano/kritaSpritesheetManager)

That Krita extension is what I used to make the example cards you see at the top of this page!

There's also this online tool that lets you turn a sequence of images into a spritesheet:

[CodeShack's Image to Sprite Sheet Generator](https://codeshack.io/images-sprite-sheet-generator/)

I have not tested how spritesheets made with that tool interact with this mod yet, but looking at the exports, it seems like it'd work!


#### GIF to Spritesheet
*"Wait, a GIF? Can I use a GIF with this mod? How?"*.
The simple answer is: Yes, you can!
I haven't implemented a way for the mod to 'read' GIF files because GIF is a wonky file format and hard to split into frames, but there are many good online tools that get the job done.

A favorite of mine is [Ezgif's GIF To Spritesheet Converter](https://ezgif.com/gif-to-sprite). It's wonderful!
And I can confirm that the spritesheets made with that website work fine with this mod. Just please remember to crop the GIF to the right size and not add any padding between frames.

## Frame Rate
The frame rate is, put simply, how fast your animation plays. The higher the frame rate, the faster your animation will play.

1. Frame rate cannot be less than or equal to 0.
2. Frame rate cannot be higher than 60.
3. The **ideal frame rate** is **24 or less**. Anything higher than that can lead to glitches and/or performance drops, so please use this carefully!

The last bullet point is extremely important. Please don't try to make fancy 60ps animations with this mod. Seriously. I have no idea what that will even do.

## Frame Count
You should inform the exact number of frames your animation has; that is, the number of sprites in your spritesheet.

*"... But why?"*
Because if your spritesheet has ***blank spaces***, informing the correct frame count lets this mod ***skip those blank spaces*** and divine your spritesheet correctly.

Gladly, figuring out how many frames your animation has is easy even if your spritesheet is a very big one. All you have to do is use some simple arithmetic:

1. Multiply the number of rows in your spritesheet by the number of columns.
2. Take that result and subtract from it the number of empty spaces in the last row.

And boom, you should have your exact frame count, ready to use.

*"But... But I don't like math!"*
I'm sorry! It has to be this way, I'm afraid. Until I find a way for my mod to *automatically* skip empty spaces somehow (which I have no idea how I would implement without iterating through every pixel, which is not a viable option for large spritesheets).

#### "Do I have too many frames?"
The nifty thing about using a spritesheet is that loading time is *drastically* reduced compared to loading each frame from the hard disk one at a time!
Since only one image is being loaded and then divided into frames, you can have *a lot* of frames with no loading time issues.

This being said: Please don't use spritesheets with 500+ sprites or anything like that. I have not tested how the mod even responds to that. My guess is that it *will* still work, but will take a while to load. So... Maybe don't do that.

## Has Pause
An optional field: Set it to 'true' if your mod uses a pause between animations.

If this field is false, the value of 'PauseTime' will always be ignored.

## Pause Time
The gap of time between each 'replay' of your animation, ***in seconds***.

The bold text is important. This means if you set PauseTime to 1, it will wait 1 whole second, as opposed to one frame.
It also means you can make it wait *half* a second by setting its value to "0.5".

In general, PauseTime should be used for any animations that need a pause between replays, just so you don't have to repeat the same frame over and over to achieve the same effect.

PauseTime is especially useful for blinking animations.



# Blink Animation Tutorial

I think the simplest kind of animation you can do, in general, is a blinking animation. I tried to make that as easy to make as possible with this mod.

Blinking animations are also the least costly performance-wise, so they're heavily encouraged!

So, walking you through the steps:

#### Make a Spritesheet
Make a spritesheet with two frames: A blinking one and a non-blinking one. The blinking sprite should come ***first*** in the spritesheet. This is important, as this mod reads sprites left-to-right, and you don't want the animation to pause on the blink.

And here's an example of a simple spritesheet for a blinking animation:

![Blink Spritesheet Example](https://i.imgur.com/OEHOZEk.png)

#### Writing the JSON File
I'm using JSON for this example, since it's the most accessible option to use this mod with.

For this blinking animation, I'll make a new JSON file for my 'Kels_Example' card.
```json
{
  "CardName": "Kels_Example",
  "Spritesheet": "Blink_Spritesheet.png",
  "FrameRate": 6,
  "FrameCount": 2,
  "HasPause": true,
  "PauseTime": 3
}
```

I have set **HasPause** to true and **PauseTime** to 3 so that there'll be a nice pause between the blinking animation replays—I don't want the character to be blinking constantly over and over.

I have also set the **FrameRate** to 6, so that the blink won't happen too fast!
And that's all you would need for a simple blinking animation. c:

# Known Bugs
1. Any card modification done to a card with an animated portrait from this mod, be it a stat boost or a sigil upgrade, will appear instantly instead of waiting for the card to flip. This is purely a visual bug.
2. I have not tested how this mod interacts with the Mycologist. LOL. I'm burnt out. This is still in beta.

# Credits
This project uses [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) for parsing JSON data.

Newtonsoft.Json's license can be found [here](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md).