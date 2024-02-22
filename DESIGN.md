## User Stories
* The Player controls their ship
	* The ship can be rotated left-right
	* The ship can accelerate forward (no instant max speed)
	* The ship will drift if no input is provided
	* The ship is wrapped around the field
	* The ship can fire one of its two weapons
		* Basic attack fires a bullet
		* Special attack fires a laser
	* If the ship collides with an asteroid / UFO, the session ends
* The field wraps around all entities: the Player's and their bullets, asteroids, UFOs
* The Game spawns asteroids and ufos
* Asteroids move with constant speed and direction
	* If an asteroid is hit with a bullet, it splits into 2 smaller asteroids (this can only happen once)
* UFOs try to collide with the Player
* Destroying an enemy adds Score points
* Session UI
	* Current Score
	* Special attacks counter
	* Special attacks recharge timer
* Defeat UI
	* Final score
	* "Press x to restart"

### User Stories++
* Start UI
	* Local high score
	* Enemies - no, ship - yes
	* "Press x to start the game"
	* Controls
* UIFX
	* Score text tween
* SFX, VFX
	* Bullet attack
	* Laser attack
	* Asteroid destroyed
	* UFO destroyed
	* Player destroyed
* ScreenFX (might be fun to add some annoying chromatic abberation)
	* Laser attack
	* Player's death

## Flow
* Start screen
* Gameplay screen
* Lose screen