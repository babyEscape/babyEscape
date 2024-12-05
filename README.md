# Baby Escape

Watch playthrough here ⌄

https://drive.google.com/file/d/1LO-9rGwpv49inNAEJKdXiFVq7cvByOV9/view?usp=sharing

### Locomotion
The main method of locomotion is Grab Movement. We chose this form of locomotion as it contributes to the player’s spatial presence. Grab movement helps the player embody the game’s character, leading to a higher sense of presence. As you are playing as a baby, it is intuitive to move around the world by crawling. Through grab movement, we also minimise Sensory Conflict ([Huber et al.](https://doi.org/10.1109/vrw58643.2023.00305)). This is because we are using real movements for congruent self motion; movements match player expectations ([Huber et al.](https://doi.org/10.1109/vrw58643.2023.00305)). 

We applied a 1.5x gain to the movement. During playtesting, we found that no gain required lots of movement to navigate the map, but applying a higher level of gain reduced immersion. 1.5x gain maintained immersion while also reducing excessive movements.

Another form of locomotion we used is climbing. Similarly to grab movement, the player can grab specific interactables around the scene and move their position vertically. Using real movements, allows for congruent self motion. There is also a tunnelling vignette that is applied to the player's vision when they fall ([video showcase - Assassin's Creed VR](https://www.youtube.com/watch?v=U88_ExuCQTk)). This aids the player with cyber-sickness, as it constrains the player's field of view to help counter unwanted movement in their peripheral vision ([Teixeira and Palmisano](https://doi.org/10.1007/s10055-020-00466-2)).

### Perceptual and sensory manipulations
To immerse the player in the role of a baby, we scaled down the player's size and enlarged surrounding objects, altering the body schema. This manipulation allowed us to transform regular objects into puzzles that needed solving, for example, pressing a button to turn on a washing machine would be no challenge for a regular adult, however for a baby it would be challenging. A regular kitchen drawer now becomes a massive obstacle that the baby needs to climb to reach their goal.

Leveraging XR capabilities, we allowed the player to stick a fork into an electrical socket which was a safe yet impactful interaction impossible outside XR. Sensory effects like electrical sound and sparks enhance the realism of the action.

By allowing players to impact their environment, for example, making the box of carrots fall and having to eat the carrot to unlock climbing or changing the look of the first person view to include the pacifier when they put it in their mouth, we give the player a high sense of presence ([Witmer and Singer](https://psycnet.apa.org/doi/10.1162/105474698565686)).

We implemented captions for the robot's clues, positioning them to assist without distracting the player. Consuming the carrot triggers visual and audio feedback, increasing immersion.

### XR-specific adapted object and world interactions
Puzzle design began with establishing the world’s theme, ensuring puzzles aligned with the context. Grab locomotion, climbing interactables, and other toy-specific interactions, like pulling a robot toy’s string, are communicated through environmental context to enhance spatial presence.

After crawling through the kitchen with Grab Move, the player reaches a dummy and can place it in their mouth as an XR Socket Interactable since they are a baby. The fork and electrical socket work similarly. Through embodiment and environmental storytelling, players connect that the dummy shoots out with microphone input. The dummy respawning in the mouth socket informs the player of its relevance. The red button on the washer designed for cognitive recognition has a collision trigger to be hit by the dummy.

When grabbing a carrot, players recall the ability to interact with objects with their mouth. After the carrot enters the head collision sphere, it is destroyed and a GUI tooltip shows up informing the player of their new climbing abilities which guides them to the next stage of the game up the climbable drawers. When the player lets go of a climbable interactor, the tunnelling vignette script calculates their y-velocity and decreases their FOV accordingly to reduce cyber-sickness.

### Other "value-added" features
Our game is made unique by its unique type of locomotion, range of interactions including via the microphone, and the models we took from [poly.pizza](https://poly.pizza/). The most stand-out element of our project, however, is its storyline and the way we reinforce it. 

In the game, the player is a baby who wants to put a fork in an electrical outlet (inspired by[Who's Your Daddy](https://store.steampowered.com/app/427730/Whos_Your_Daddy/)), and we guide them to this goal using various post-it notes that still fit into the world of the game. For example, we created a post-it note that says “Make sure little Jimmy eats his veggies so he can grow strong”, hinting at a later puzzle where the player needs to eat carrots to grow strong enough to climb. This both reinforces the story and setting of the game, whilst also providing a hint to the player. 

We also create this story using various audio files, including some original audio that plays from a toy robot which encourages them to pursue the main goal of the escape room when the player pulls its string. Additionally, when the player completes the goal, the robot visits them again and congratulates them on beating the game, providing an end to the story.
