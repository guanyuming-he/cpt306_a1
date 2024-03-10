# Game
Two levels of the same map, same kinds of enemies (different in numbers and behaviours). 

## Fields
The game has
1. A `Hero`.
2. A list of `Enemy`s.
3. A list of `Obstacle`s.
4. An `EnemySpawner` to spawn new enemies.
5. A `StateManager` to manage the game's state.
6. A `UIManager` to manage the UI drawn on the screen.

## Initialisation
The game has an `init()` method that inits the persistent objects:
1. The `StateManager` is created.
2. The `UIManager` is created.

`init()` is called the first time the game object is created. 

The game has a `reset()` method that resets the level-dependent objects:
1. Takes parameters of 
    - i. a configured `ObstacleSpawner` to spawn obstacles. All parameters of spawning obstacles, except the map, are given to it in its configuration.
    - ii a configured `HeroSpawner` to spawn the hero. All parameters of spawning the hero, except the map with the spawned obstacles,  are given to it in its configuration.
    - iii. a configured `EnemySpawner` to spawn enemies. All parameters of spawning enemies, except the map with the spawned obstacles and hero, are given to it in its configuration. 
2. First, it destroys all the objects on the map:
    - All except the `UIManager` and the `StateManager` are destroyed, if they are not `null`.
3. Now the level is empty. It recreates the objects with the spawners.
    - i. It passes the empty map to the `ObstacleSpawner`, takes the obstacles spawned by it,
    and puts them in the map (the last step may be unnecessary as taking the obstacles may automatically mean having them in the map).
    - ii. It takes the `EnemySpawner`.
    - iii. It passes the map with the obstacles to the `EnemySpawner` and takes the enemies spawned by it.
    - iv. It passes the map with the obstacles and the enemies to the `HeroSpawner` and takes the hero spawned by it.
4. It binds the new `Hero` to the `UIManager` so the UI can display the hero's status.

The game has a `start()` method that is called when the user starts/restarts the game.
1. It reads the settings of `Level 1` from somewhere.
2. It passes the settings to `reset()` to create the level-dependent objects.
3. It resets the `StateManager` by calling the manager's `reset()`.
    - Note: This will resets the score, the time, and all information about the previous game session. Do save them just after the game ends.
4. It starts the game by calling the `StateManager`'s `start()`.
    
## Update
Inside the `Update()` method of the game object,
1. If it sees that the `StateManager`'s state is `RUNNING`, 
    - i. it checks if the hero has died by checking if its health <= 0.
    If so, it calls the manager's `gameOver()` to switch the state.
2. If it sees that the `StateManager`'s state is `GAME_OVER`, then
    - It pauses all and use the `UIManager` to bring up game over UI.
3. If it sees that the state is `VICTORY`, then
    - It pauses all the use the `UIManager` to bring up victory UI.
4. If it sees that the state is `NEXT_LEVEL`, then
    - It pauses all the use the `UIManager` to bring up the next level UI.
5. If it sees that the state is `PAUSED`, then it calls the `UIManager` to present
the level starting UI (waiting for I'm ready button to be pressed).


# State Manager
Fields:
1. an enum Level that describes which level is being played.
    - LEVEL_1
    - LEVEL_2
2. an integer `score`

States:
1. `PAUSED`
When the user paused the game. He can resume on the UI.
2. `RUNNING`
When the user is playing in a level.
3. `GAME_OVER`
When the user has failed. He can return to the main menu.
4. `VICTORY`
When the user has won. The ranking is displayed. He can return to the main menu.
5. `NEXT_LEVEL`
When the user has succeeded on the first level. He can the choose to go to the next level.

Note: on the main menu the game object is not created yet. So there is not a state for this.

![The state machine diagram](image.png)

# Base Classes & Interfaces
## Abstract class `LevelObject`
Is the base class of any object that can be placed in a level.

1. Has a size $(w, h) \in \mathbb{R}^2$ (May be provided by Unity)
2. Has a position $(x, y) \in \mathbb{R}^2$ (May be provided by Unity)

## Abstract class `MovingObject`
Is the base class of any level objects that can move.

1. Inherits from `LevelObject`
2. Has a unit direction vector $(d_x, d_y) \in \mathbb{R}^2$ (May be provided by Unity).
3. Has a speed scalar $s$ (May be provided by Unity).

### Update
Is abstract. Bullets may just go, but Enemies and Heroes cannot go through `Obstacle`s.

### Other Methods
Has a non-virtual method `nextFramePos()` that returns the position it would be in the next frame if
not considering anything but its current position, speed, and the game boundary.
- $(x',y') = (x,y) + \Delta t \cdot s \cdot (d_x,d_y)$ 
- But rounded inside the game boundary, considering its size as well. That is,
    $$
    0+\frac{w}{2} \le x' \le 23-\frac{w}{2}\quad \wedge \quad
    0+\frac{h}{2} \le y' \le 23-\frac{h}{2}
    $$

## Abstract class `Attack`
### Fields
1. Has an immutable float `damage`.
2. Has an immutable float `timeBetweenAttacks`, that decides the minimum number of seconds between two attacks.
3. Has a float `cooldownVal`, that records the number of seconds elapsed after the last attack.
4. Has a boolean `inCooldown`

### Init
1. `damage` and `timeBetweenAttacks` are given through the constructor
2. `inCooldown = false` and `cooldownVal = 0.0f`

### Update
1. If `inCooldown`, then `cooldownVal += dt`.
    - If `cooldownVal >= timeBetweenAttacks`, then 
        - `inCooldown = false`
        - `cooldownVal = 0.0f`

### Other methods
- method `attack(x, y)`.
    - 1. if `inCooldown = true`, then returns.
    - 2. calls `onAttack(x, y)`, which does what the attack does.
    - 3. `inCooldown = true`

- abstract method `onAttack(x, y)` does what the attack does at the location $(x,y)$
For example, hit anything in a range over $(x,y)$, or fire a bullet from $(x,y)$.

# Hero
<!-- TODO: add a basic class for all objects that have health and can be hit 
Then inherit Hero from it. -->

## Fields
1. A `MeleeAttack` attack component.
2. A `RangedAttack` attack component.
