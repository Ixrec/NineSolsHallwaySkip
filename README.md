# Nine Sols Hallway Skip

A Nine Sols mod that lets you skip the long hallway fight sequence after the Point of no Return. This is an offshoot of https://github.com/Ixrec/NineSolsCutsceneSkip after I discovered that the hallway fight is implemented as a cutscene, and skipping the "cutscene" happens to also skip all of the enemy spawning without breaking anything.

The keybind for actually skipping can be changed (if you're new to Nine Sols mods, press F1 to change settings like that).

This is what it looks like:

![Demonstration: Skipping the Hallway Fight](https://github.com/Ixrec/NineSolsHallwaySkip/blob/main/hallway_demo.gif?raw=true)

When combined with Cutscene Skip, you can even skip the short cutscenes before and after the fight.

## Contribution / Development

Should be the same as any other Nine Sols mod. See https://github.com/nine-sols-modding/NineSols-ExampleMod for detailed instructions.

## What about Cheat Menu?

The skip feature in Yukikaco's CheatMenu will simply skip anything it can find in the current level with a `.TrySkip()` method. This is sometimes useful for developers, but for normal gameplay this is dangerous and will softlock in dozens of places.
