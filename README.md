# Project for Gesture Based UI Development - Bad Jack

![Image not found](http://puu.sh/odr5p/c524eb56f7.jpg)

**Badjack was an experimental project to try develop with hardware I've never used before and using gestures outside of what's normally used. Therefore I developed a simplified version of BlackJack where you play against the dealer.**

## Contents

* Video
* Requirements
* Installation
* How to play
* Technologies used

## Video

Click image below to view the video

[![Video](http://puu.sh/odr5p/c524eb56f7.jpg)](https://www.youtube.com/watch?v=lctQvd9nnng&feature=youtu.be&ab_channel=0Xian0)

## Requirements

The game was developed using the Google Cardboard SDK and meant to be played on an Android device with high pixel density and on a newish phone. The phone I tested this project on was a Sony Xperia Z3 with a screen size of 5.2inches and a resolution of 1080 x 1920 pixels.

* Android Device with Lollipop preferabley
* Google Cardboard Headset I used was similar to these [Link](http://www.amazon.com/s/ref=nb_sb_noss?url=search-alias%3Dmobile&field-keywords=google+cardboard)

## Installation

Move the apk file named "Blackjack.apk" from the root directory to your android device through USB. Make sure your android device is set up to allow development apks. Navigate to the folder where you stored the apk and run it.

## How to Play

This game works off **two** head gestures

* Nodding in agreement (Moving head up and down)
* Shaking head in disagreement (Movind head from side to side)

The game is driven by asking you a series of yes and no questions

The game greets you with a welcome message and asks you to nod your head in agreement and shake your head in disagreement. This is just to test that the device is picking up on the head movements. The screen when then freeze briefly while the level is being loaded.

The game loop is as follows

**Loop**
* Do you wish to bet
* Do you wish to take a card

To leave this loop you choose the option **to stick** and this ends the current hand. 

Once the player runs out of money or the computer. You are placed in a room just to signify the game is over with either You Win or You Lose printed on the walls.

## Technologies used

* [Unity3D 5.2](https://unity3d.com/)
* [Google Cardboard SDK for Unity](https://developers.google.com/cardboard/unity/)
