# lil guyz
Can you cope up with these lil guyz?

**lil guyz** is a 2D puzzle game game resets in 10 seconds, and your past turns come back to help you. Each time you die, the next turn spawns a clone of you, that repeats exactly what you did before. Use the clones to press pressure plates, open doors and collect the orb.

## How it works

- You have **10 seconds** per turn. When the timer hits zero, you die.
- After a short countdown, a new turn starts and your previous run comes back as a **clone**.
- Each clone plays back the exact movement from its turn, then disappears **0.25 seconds earlier** than the one before it. The newest clone lasts 9.5 seconds, the next lasts 9 seconds, and so on.
- Up to **10 clones** can be on screen at once. Once you have more than 10 past turns, the oldest one drops off.
- Clones can stand on pressure plates, so you can use an old run to hold something down while you do the next part.

## Controls

| Action | Key |
| --- | --- |
| Move | Arrow keys or `WASD` |

Moving is what starts the timer. Standing still doesn't count as a turn.

## Getting started

1. Clone the repo:
   ```bash
   git clone https://github.com/kabirutreja/lil-guyz.git
   ```
2. Open the folder in **Unity** using Unity Hub.
3. Open the main scene in `Assets/` and press **Play**.


### Main scripts

- **`Recorder`** saves each turn's movement and spawns clones for past turns.
- **`playerMovement`** handles movement, animation, and the player's death.
- **`Timer`** runs the 10-second countdown and shows it on screen.
- **`pressureplate`** tracks who is standing on it and lights up when pressed.
- **`CameraControl`** follows the player and resets when a new turn starts.
- **`GameManager`** sends the events that tell the other scripts when a turn starts, ends, or respawns.
- There are multiple scripts which are not main but used to make the gameplay.

## Status

This is a work in progress or just a demo. The same mechanics would be used in the larger format.

## Assets Used

The game use 2D Minimal - Character Maker assets from asset store and the scripts and files from the asset are not included for producer's privacy.
You can purchase the asset from here: [https://assetstore.unity.com/packages/2d/characters/2d-minimal-character-maker-363620]
The author of this game(me) owns the right to use it commercially.

## AI usage
AI is only used in making the getting started section of the readme and a little in playerMovement script. That's it.

## License

All rights reserved by the author.
