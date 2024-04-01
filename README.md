# Nova

A rogue-like, bullet-hell, survival, RPG.

## High level

### Game-loop
The general game loop involves going through different levels, killing enemies, gaining new active and passive abilitites, leveling up your character, and making some decisions that may or may not change your relationship with NPCs or combat viability in later runs.

If not continuing an existing run, the player will choose a character to use for the new run, then start in an rest area. In the rest area, the player can initially talk to some NPCs to either gain context on how the game works or perhaps buy some equipment before they officially begin their run if they have any money.

Once the player is done in the rest area, they'll move on to the first level. If not in a rest area, the player is in a fighting area. In the fighting are, the player will fight various enemies. Fighting various enemies will grant the player XP for the current character, currency, and potentially some useful items that will help the player get stronger during their current run.

Each fighting area has an "end" state where they fight a tough enemy. The final tough enemy is guarenteed to drop an item for the player. Also, when the tough enemy dies, no more enemies spawn in that fighting area, allowing the player to take a breather, explore the area, or approach the next level. The next level could be another rest area or fight area depending on some chance. After every fighting area, the chance to encounter a rest area increases. Once a rest area is encountered, this chance is reset to baseline. 

This is the loop: use the rest area to spend currency or save the run state, then transition to fighting areas to progress your character in terms of XP and items as well as any exploration they player may want to do.

### Levels

There are two types of levels: rest areas and fighting areas.

#### Rest areas

A rest area allows players to take a break from the fighting to regain their strength, purchase any items, provisions, or save the state of their current run.

Rest areas may contain NPCs that sell you items that can empower your character as well as any items for healing. They may also give you jobs or tasks that you can take when going to fight areas so you can gain extra items or currency.

Rest areas will also allow you to save your game. What does "saving" mean in a game like this? Saving just means saving the progress you have on the current run so you can leave the game and come back where you left off. If the player dies during their run, their save of that run will be deleted.

There are two ways to encounter a rest area: 
- The player is just beginning their run.
- The player randomly encounters a rest area after going through a number of fighting areas.

#### Fighting areas
A fighting area is where a player will spend most of the game; killing enemies, looting the map, and progressing their character. 

As the player enters a fighting area, enemies will begin spawning. The player needs to kill all the enemies in the fighting area to then spawn the "boss" of the fighting area. Once the player defeats the boss, they'll get an item from the boss and proceed to the next area, which may be another fighting area or rest area depending on some random chance that increases after completing each fighting area.

Fighting areas are the sole place to take care of jobs/tasks NPCs have given as well as any exploration that they may embark on. Exploring the entire fighting area my unveil secret items the player may want to empower their character.

### Enemies

The difficulty of enemies will gradually rise as the player continues through different fighting areas and as the fighting timer increases.

### NPCs

NPCs only exist in rest areas. NPCs give an RPG opportunity where how you treat the NPCs may result in NPCs never appearing again in your run, the stock of the NPCs shop, and price of items.

NPCs may offer jobs/tasks the player can complete to get more XP, currency, or items.

There may also be random NPCs that have a chance to show that offer a limited but much more rare stock that sell items for a higher price.

### Characters

There are multiple characters that have very different playstyles. 

Three character ideas include:
- A swordsman that specializes in melee combat.
- A gunner that specializes in ranged combat using firearms.
- A magician that specializes in ranged combat using magic.

### Items

### Progression

There are two types of progression: global progression and per-run progression.

### Global progression

Some global forms of progression include currency, base character stat increase, and item loot pool expansion.

### Per-run progression

Each character has their own skill-tree that they complete. In each new run, the characters skill tree will have 0 points allocated. After gaining enough XP and leveling up, the user will be able invest points in whichever available leaf in the skill-tree. If the user is not happy with their current selection, they can undo the entire skill tree or undo specific leafs in the tree to re-allocate their skill points.

