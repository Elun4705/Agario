Author:     Emmanuel Luna
Partner:    Zhuofei Lyu
Date:       13-April-2023
Course:     CS 3505, University of Utah, School of Computing
GitHub ID:  assignment8agario-luna
Repo:       https://github.com/uofu-cs3500-spring23/assignment8agario-luna
Date:       13-04-2023
Solution:   assignment8agario-luna
Copyright:  CS 3500 and Zhuofei Lyu,Emmanuel luna- This work not be copied for use in Academic Coursework.
```

# Overview of the Chatting functionality:
    The server is currently able to keep track of indidivudal clients and propagate recieved messages among all of them.

    - Note: This is based off of theory and a brief overview by a TA.  We haven't been able to properly test this to its full extent as a result of exenuating circumstances.

# Time Expenditures:

    1. Client GUI                                 Predicted Hours:    5        Actual Hours:   6
    2. FileLogger                                 Predicted Hours:    0        Actual Hours:   0
    3. Models                                     Predicted Hours:    7        Actual Hours:   5
    5. AgarioClient                               Predicted Hours:    12       Actual Hours:   11

# On time management and estimation skills:
    Our ability to manage and predict out workload wasrather well, as I (Emmanuel Luna) took care of the majority of the orientation of the project
# Design Decisions:
    1. Our design may end up looking rather simple, as we spent most of our time trying to get the internals to work first, and mainly wanted they translation to e simplistic.

# Comments to Evaluators:
    We could connect to the school server but where immediately disconnected.

# Solution Introduction:


This Solution is used to commicate with server and get data from server. once we received data from server we should draw that on MAUI(GraphView),
the next few steps will step by step to check the process what we did in this solution:
1. we make connection with server and we use professor's networking dll.
2. once connection established, we will receiving and sending data back and forth between server and client(player).
   1) we receive a set of foods, players, dead players, eaten foods and game object from server. foods should draw on the MAUI and main player 
      and other player could eat that increasing the size if they want. players represent the players that we want to draw on MAUI.
      Dead players represent the players that they eaten by other players unit, and dead foods represent the foods that eaten by the players
      the main player is player that current player is controlled
   2) In step one, is the everything that we should draw on the GUI. Then we should send data to the server. we should send split and moving command 
      following specific protocols, if we do that the server could be konw this message is sepcial for it. and server will I should do special handling
      for this data. and then it will send the relevant data that player need. after we could make our players drawen on different places.
3. in our solution explorer, we should have three different structures, and they are Model, View and controller. In model(Model) project we have food, gameobject,
   player, and worlds, note: the protocol class let peoples know what should our protocol looks like. players class store a set of players 
   in data structure, and we need store there players into world, because world has everything about this game. And we should store a list of foods in this
   world,foods are not eaten and foods should drawen on GUI(GraphView). note: we also have GameObject class, this class is super class of food and player class
   and this class have features that not only food have also player(eg. loation, X pos, Y pos, and color). the world store all foods and players and main player
   ID, we should use world information for scene and our GUI.
4. we also have file logger that will record the steps that we did in our codes, if we have any questions about our codes, logger help us to know where problems
   is.
5. Our ClientGUI has three part: MainPage, MainPage.xamlcs and scene. mainpage just give basic arrangement for GUI, and provide event for MainPage.xaml.cs
   if player do any operation the event handler will do corresponding action and mainpage.xaml.cs also could drawing the circle right now.
   scene is most important part of this project, this file is drawable, that is why we could could method "draw" in this class and mainpage.xaml.cs could
   invoke invalidate method base on this class. scene class draw our food and players. once "invalidate" is invoked, draw method is called and graphView
   will have main player and other players and food that around main player