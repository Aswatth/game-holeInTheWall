# A Unity Game - Hole In The Wall 

[Game](https://play.google.com/store/apps/details?id=com.SAGamesInc.Holeinthewall)

<img src="/Images/SS-1.png" width="400" height="200"/> 

<img src="/Images/SS-2.png" width="200" height="400"/> <img src="/Images/SS-3.png" width="200" height="400"/> 
 <img src="/Images/SS-5.png" width="200" height="400"/>

A hyper-casual games developed using Unity Game engine.

Objective:
  - Fit oncoming obstacles by constructing blocks
  - Collect diamonds along the way

Gameplay:
  - Swipe to Up/Down and Left/Righ to construct blocks
  - Tap Left/Right side of the screen to roll the cube
  - Game ends when player fails to match the oncoming obstacle shape

Working:
<p> 
The game consists of 45 obstacales each with their own JSON file holding infomation about each block of the obstacle. At the start of the same a random obstacle is generated and based on their JSON defenition the colliders for each block of the obstacles is generated. Each block uses a box collider and determined by the JSON info, the collider can obstruct player if it is not a hole else will allow the player to pass through. Once the obstacle is generated a collectible diamond is spawned at non-hole location for the player to collect.
</p>

<p> 
The generated obstacles continues to move towards player at constant speed which will gradually increase upon progressing. The player should build the obstacle shape in order to pass through the obstacle successfully. 
  
In order to avoid below scenarios:
  
  <img src="/Images/SS-4.png" width="200" height="400"/>

The number of blocks constructed by the player is validated against the number of holes in the generated obstacles in order to ensure player has exatly matched the oncoming shape.
</p>
