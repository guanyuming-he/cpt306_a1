# Class Diagram
![The class diagram](class_diagram/class_diagram.svg)

# Game
Two levels of the same map, same kinds of enemies (different in numbers and behaviours). 

Inherits
1. `MonoBehaviour`

## Fields
The game has
- A `map: Map`
- An `enemySpawner: EnemySpawner` to spawn new enemies.
- A `stateMgr: StateManager` to manage the game states.
- A `uiMgr: UIManager` to manage the UI drawn on the screen.

## Start
- The `StateManager` is created.
- The `UIManager` is created.
    - The main UI is brought to the screen.

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
5. If it sees that the state is `disabled`, then it calls the `UIManager` to present
the level starting UI (waiting for I'm ready button to be pressed).

## Methods
A `resetLevel(int levelNum)` method that destroys and recreates all the level-dependent objects:
- First, it destroys all the level objects (i.e. All except the `UIManager` and `StateManager`):
    - if they are not `null`.
- Now the level is empty. It recreates the objects with the spawners, according to levelNum.
    - It passes the empty map to the `ObstacleSpawner` and `DesObsSpawner`, takes the obstacles spawned by it,
    and puts them in the map (the last step may be unnecessary as taking the obstacles may automatically mean having them in the map).
    - It passes the map with the obstacles and the enemies to the `HeroSpawner` and takes the hero spawned by it.
    - It binds the new `Hero` to the `UIManager` so the UI can display the hero's status.
    - It passes the map with the obstacles to the `EnemySpawner`s and takes the enemies spawned by it.

A `startGame()` method that is called when the user starts/restarts the game from the main UI.
- It reads the settings of `Level 1` from somewhere.
    - uses them to configure the `Spawner`s
    - and calls `resetLevel()` to create the level-dependent objects.
    - calls `stateMgr.startGame()`

A `continueGame()` method that is called when the user continues to the next level from the level passed UI.
- It reads the settings of `Level 2` from somewhere.
    - uses them to configure the `Spawner`s
    - and calls `resetLevel()` to create the level-dependent objects.
    - calls `stateMgr.continueGame()`

# State Manager
## Fields:
1. an integer Level that describes which level is being played.
2. an integer `score`
3. A `levelTimer: Timer` that manages the level's time.

## States:
- `MAIN_UI`
When the user is at the main UI.
- `RUNNING`
When the user is playing in a level.
- `PAUSED`
When the user paused the game. He can resume on the UI.
- `NEXT`
When the user has succeeded on the previous (first) level. Now a "Next level" UI is displayed to him.
He can the choose to go to the next level from there.
- `GAME_OVER`
When the user has failed. He can return to the main menu.
- `VICTORY`
When the user has won. The ranking is displayed. He can return to the main menu.

Note: on the main menu the game object is not created yet. So there is not a state for this.

![The state machine diagram](state_diagram.svg)

## Methods:
- `startGame()` starts the game from `MU` to `R`. It inits `level := 1, score := 0`.
- `goHome()` can be called from any state that is not `MU, R`. The method switchs the state back to `MU`.
- `pause()` brings state from `P` to `R`
- `resume()` brings state from `R` to `P`
- `continueGame()` brings state from `N` to `R`. Also `++level`. It's called at `N` when the user chooses to continue from the UI.
    - also resets the `levelTimer`.
- `nextLevel()` brings state from `R` to `N`. It's called when the current level is complete and the user can go to the next level.
    - `private` as only the manager can decide if a level has completed or not.
- `win()` brings state from `R` to `V`, **and records the score field into a file**.
    - `private` as only the manager can decide if the game has won or not.
- `gameOver()` brings state from `R` to `GO`.
- `onLevelTimerFired()` checks `level`.
    - `level = 1 -> Game.nextLevel()`
    - `level = 2 -> Game.win()`

## Update
- if `state != R`, then do nothing.
- otherwise
    - calls `levelTimer.update()` with `dt`.

# Map
A map stores all level objects in a level.

## Fields
- A public `Hero`, can be null if not created yet.
- A public list of `Enemy`s, can be empty if not created yet.
- A public list of `Obstacle`s, can be empty if not created yet.

## Methods
- `clear()` destroys all objects in the map (all that are not null).
- `addHero(hero: Hero)` sets `this.hero` to `hero` iff `this.hero != null`.
- `addEnemy(enemy: Enemy)` adds an enemy to the `Enemy` list.
- `addObstacle(obs: Obstacle)` adds an obstacle to the list.

# Base Classes & Interfaces
## Abstract class `LevelObject`
Is the base class of any object that can be placed in a level.

### Fields
- A protected `rigidBody: Rigidbody2D` that stores the `Rigidbody2D` component assigned to the level object. Every level object in the game must have a `Rigidbody2D`, so this field cannot be null.

### Start
- virtual so that the base's is called before all.
- `rigidBody = gameObject.GetComponent<Rigidbody2D>();`
- asserts that `rigidBody != null`.

### Methods
- `destroy()` that destroys the `gameObject` containing it.

## Abstract class `MovingObject`
Is the base class of any level objects that can move.

1. Inherits from `LevelObject`
2. Has a unit direction vector $(d_x, d_y) \in \mathbb{R}^2$
3. Has a speed scalar $s$.

### Init
`speed := 0`, and `direction` can be any unit vector.

### Update
Sets the velocity of the `Rigidbody2D` to $s(d_x, d_y)$.

## Abstract class Enemy
Inherits from
1. `MovingObject`
2. `HittableObject`

### Fields
1. An `Attack` component

### Methods
- TODO: subclasses: `takeDamage()` if it's killed, then add scores to `StateManager`.

### Update
- `dead() -> destroy()`.

## Abstract class Obstacle
Inherits from `LevelObject`

## Abstract class `Attack`
### Fields
1. Has an immutable int `damage`.
2. Has a `cdTimer: Timer` .
4. Has a boolean `inCooldown`.

### Init
1. `damage` is given through the constructor
2. `cdTimer` is inited with cd value, `cdComplete()`, disabled=true, and loop=false.
3. `inCooldown = false`.

### Update
- `cdTimer.update(dt)`.

### Other methods
- method `tryAttack(x, y)`.
    - 1. if `inCooldown = true`, then returns.
    - 2. calls `onAttack(x, y)`, which does what the attack does.
    - 3. `inCooldown := true`
    - 4. `cdTimer.enable()`. `cdTimer.reset()`.

- abstract method `attack(x, y)` does what the attack does at the location $(x,y)$
For example, hit anything in a range over $(x,y)$, or fire a bullet from $(x,y)$.

- `cdComplete()` called by the timer.
    - inCooldown := false

## Interface `IHittable`
Has an integer property `health`.

### Methods
1. Has a method `onHit(dmg: int, src: LevelObject)`
    - i. It reduces `health` by `dmg`
    - ii. It calls `takeDamage(dmg: int, src: LevelObject)`
2. Has a virtual method `takeDamage(dmg: int, src: LevelObject)` whose default impl does nothing.
3. Has a final method `dead()` that returns `health <= 0`.

# Important Helper classes
## Timer
1. has a `disabled` boolean field.
2. has a `fired` boolean field.
3. has an immutable `loop` boolean field.
4. has an immutable `fireTime` float field.
5. has a `timeElapsed` float field
6. has an immutable `onFire` field that references a function.

## Ctor
1. `disabled` is passed in as an argument.
2. `fired = false`
3. `loop` is passed in as an argument.
4. `fireTime` is passed in as an argument.
5. `timeElapsed = 0.0f`.
6. `onFire` is passed in as an argument.

## Methods
- `hasFired()` returns `fired`
- `resetTimer(enable: bool)` resets the timer so that 
    - `fired = false`, `timeElapsed = 0.0f`.
    - `enable -> enableTimer()`
- `disableTimer()` and `enableTimer()` do what their names say.
- `update(dt: float)` is not called by Unity. Must be called in an Unity Object's `Update()`.
    - if `disabled`, then do nothing
    - otherwise, if `fired`, then do nothing.
    - otherwise, `timeElapsed += dt`.
        - If `timeElapsed >= fireTime`, then set `fired = true` and call `onFire`.
        - If `loop = true` then call `resetTimer()`.

# Objects

## Hero
Inherits from
- `MovingObject`
- `HittableObject`

### Fields
- A Shooting crossbar (`gameObject`) that points at where the heros shoots at.
- A `MeleeAttack` attack component.
- A `HeroRangedAttack` attack component.

# Attacks
## MeleeAttack
Inherits from `Attack`.
**ImplNote** Use `Physics2D.BoxCast`.

### Fields
Has an `attackRange`: $(w,h) \in \mathbb{R}^2$.

## Ranged Attack
Inherits from `Attack`.

### Fields
Has a `projectileSpawner: ProjectileSpawner`.

### Ctor


### Methods
- Overrides `attack(x,y)`:
    - a `Projectile` is spawned using the `projectileSpawner` at $(x,y)$.