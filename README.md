# Project Title

## Overview
This project is a Unity-based card game that implements move and undo functionality, card stack management, and drag-and-drop interactions. The architecture is designed to be modular, scalable, and easy to maintain, with clear separation of concerns between services, UI, and game logic.

## Features
- **Move and Undo Functionality**: Tracks card moves and allows undoing the last move.
- **Card Stack Management**: Handles adding, removing, and peeking cards in stacks.
- **Drag-and-Drop Interaction**: Enables intuitive card movement with visual feedback.
- **Service-Oriented Architecture**: Centralized service management for game logic and UI.
- **Error Handling**: Displays error popups for invalid actions.

## Architecture
The project follows a service-oriented architecture with the following key components:
- **MoveService**: Manages card moves and undo functionality.
- **CardService**: Handles card initialization and stack setup.
- **UIService**: Manages UI popups and error handling.
- **ServiceProvider**: Centralized registry for accessing services.
- **Card and CardStack**: Core game objects representing cards and their stacks.
- **CardDragController**: Handles drag-and-drop interactions for cards.

## Unity Version
This project was developed using **Unity 2022.3.60f1**. Ensure you are using this version or a compatible one to avoid compatibility issues.

## Screen Size
The game is optimized for iPhone screen sizes, specifically **1125 x 2436** (portrait mode). Can work in other resolutions as well. It is recommended to start the game with this resolution for the best experience.

## How to Run
1. Open the project in Unity.
2. Set the Game View resolution to **1125 x 2436** in the Unity Editor.
3. Ensure all dependencies (e.g., `DOTween`, `Cysharp.Threading.Tasks`) are installed.
4. Press Play in the Unity Editor to start the game.


## Editor Scripts
The project includes custom editor scripts to streamline development:

- **CardSOCreator**: A custom editor window for creating a `ScriptableObject` that holds card data by reading card sprites from a specified directory.
- **CardPrefabCreator**: A custom editor window for generating card prefabs using the card data and saving them to a specified directory.

These tools can be accessed from the Unity Editor under the `Tools` menu, making it easier to manage card assets and prefabs.

## Future Improvements
- Add support for multi-card dragging.
- Implement additional game rules and logic.
- Enhance UI/UX with animations and sound effects.

## Conclusion
This project demonstrates a thoughtful approach to Unity game development, with a focus on modularity, clarity, and maintainability. The use of AI tools streamlined the development process, ensuring high-quality code within a limited timeframe.
