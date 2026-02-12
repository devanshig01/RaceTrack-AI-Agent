# RaceTrack AI Agent

A Unity-based racing simulation featuring a fuzzy logic AI agent that races on a procedurally generated track. The agent aims to maximize speed without crashing, getting stuck, or turning around.

## Demo

https://github.com/devanshig01/RaceTrack-AI-Agent/assets/68164303/a0ceabf7-e058-4de2-8bb5-0c890b44b119

## Prerequisites

- **Unity Editor 2021.3.22f1**
- **Visual Studio 2020+** with the **Managed Game** workload (`Microsoft.VisualStudio.Workload.ManagedGame`)
- **Unity ML-Agents** package `2.0.1` (resolved automatically via `Packages/manifest.json`)
- **Unity Test Framework** `1.1.33` (resolved automatically via `Packages/manifest.json`)

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/devanshig01/RaceTrack-AI-Agent.git
   ```
2. Open the project in **Unity Hub** or directly in **Unity Editor 2021.3.22f1**.
3. Let Unity resolve packages automatically from `Packages/manifest.json`.
4. Open the main scene: **Assets/Scenes/RaceTrackFZ.unity**.
5. Press **Play** to run the fuzzy logic AI agent.

## Project Structure

| Directory | Description |
|---|---|
| `Assets/Scenes/` | Racing environment scenes (RaceTrackFZ, DragRaceTrackFZ, RaceTrackHuman, RaceTrackNN) |
| `Assets/Scripts/` | Core vehicle logic (`AIVehicle.cs`, `AIVehicleNN.cs`) and game management |
| `Assets/Scripts/GameAIStudentWork/` | Fuzzy logic agent (`FuzzyVehicle.cs`), neural network agent (`NNVehicle.cs`), and tests |
| `Assets/ML-Agents/` | ML-Agents training timer logs |
| `Assets/MLModels/` | Trained neural network models |
| `Assets/Prefab/` | Reusable game components |

## Scenes

| Scene | Description |
|---|---|
| **RaceTrackFZ** | Primary fuzzy logic racing scene |
| **DragRaceTrackFZ** | Drag racing variant |
| **RaceTrackHuman** | Human-controlled baseline |
| **RaceTrackNN** | Neural network agent testing |

## Running Tests

1. Open the **Unity Test Runner** via **Window > General > Test Runner**.
2. Tests are located in the `RacingTests` assembly under `Assets/Scripts/GameAIStudentWork/RacingTests/`.
3. Run the `TestFuzzyRace` test to validate the fuzzy logic agent's racing performance.

## Built With

- [Unity](https://unity.com/) 2021.3.22f1
- [Unity ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents) 2.0.1
- [Tochas.FuzzyLogic](https://github.com/DavidAlpworthy/Tochas.FuzzyLogic) - Fuzzy logic library

## Acknowledgments

Developed in the class CS 7632: Game AI taught by Jeff Wilson, Georgia Tech
