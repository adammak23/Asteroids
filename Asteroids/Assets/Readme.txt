To adjust game constants go to GameScene:

Variable names are Self-explanatory

Spawner (Object):
┗Spawner (Script):
  ┣ Number of Asteroids - initial number of asteroids
  ┣ Increment Asteroids Per Lvl - percentage of increment per lvl
  ┣ Initial Max Thrust - Asteroids start with (-x, x) range
  ┣ Initial Max Turn - etc.
  ┗ Percentage Offset Outside Screen - Region of asteroids spawn

Spaceship (Object):
┗Controls (Script): Thrust, Turn, Bullet Force

PlayerScore (Object):
┗Score (Script):
  ┣ Big Asteroid Points
  ┣ Medium Asteroid Points
  ┣ Small Asteroid Points
  ┣ Respawn Time (in seconds)
  ┗ Lives