![Gameplay GIF](https://ben-dodd.com/assets/images/projects/csc384project/gameplay.gif)

## Overview

This project was created for the CSC384 Video Games Programming module at Swansea University. The aim of the project was to create a video game in Unity with C#, from a selection of game genres. I chose to create a Frogger-style game, where the player must navigate a character across a busy road and river. Instead, I themed my game around a virus trying to infect computers, by crossing the paths of files and programs.

## Features

- Achievements
- Leaderboards
- Profile System
- Settings

## Achievements

![Achievements](https://ben-dodd.com/assets/images/projects/csc384project/achievements.jpg)

The achievements system uses a JSON file to store the different achievements. The achievements are loaded into the game and displayed in the achievements menu. They are unlocked by completing certain tasks in the game, such as completing a level or not dying in a level.

## Leaderboards

![Leaderboards](https://ben-dodd.com/assets/images/projects/csc384project/leaderboards.jpg)

Players can compete against each other to get on the leaderboard. The leaderboard is for local players only.

## Profile System

![Profile](https://ben-dodd.com/assets/images/projects/csc384project/profiles.jpg)

Players can set their name, and choose a colour for their virus character.

## Service Locator Pattern

I used the service locator pattern for loading and saving player data. This allows the player data to be accessed from anywhere in the game, without having to pass the data between scenes. This is useful for the achievements and leaderboards, as they can access the player data from anywhere in the game.

## Command Pattern

I used the command pattern for the player input. This allows the player input to be changed at runtime, without having to change the code. In the game you have the ability to rewind time, which changes the player input to the rewind input. This allows the player to move backwards in time, and undo their mistakes.

## Gallery

![Main Menu](https://ben-dodd.com/assets/images/projects/csc384project/mainmenu.jpg)
> The main menu of the game.

![Death Screen](https://ben-dodd.com/assets/images/projects/csc384project/death.jpg)
> The death screen of the game.

![Level](https://ben-dodd.com/assets/images/projects/csc384project/level.jpg)
> A level of the game.
