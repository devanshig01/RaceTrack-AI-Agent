# RaceTrack-AI-Agent

![Unity](https://img.shields.io/badge/Unity-2021.3.22f1-blue) ![ML-Agents](https://img.shields.io/badge/ML--Agents-2.0.1-green)

Implementation of a fuzzy logic agent that races on a procedurally generated track. The agent uses fuzzy logic to make driving decisions — steering, throttle, and braking — based on sensor inputs from the track environment. The aim is for the agent to go as fast as possible without crashing, getting stuck, or turning around. A neural network agent variant trained with Unity ML-Agents is also included for comparison.

https://github.com/devanshig01/RaceTrack-AI-Agent/assets/68164303/a0ceabf7-e058-4de2-8bb5-0c890b44b119

## Prerequisites

- **Unity Editor** — [2021.3.22f1](https://unity.com/releases/editor/whats-new/2021.3.22)
- **Visual Studio 2020+** with the `ManagedGame` workload (configured via `.vsconfig`)
- **Unity ML-Agents** package **2.0.1** (resolved automatically from `Packages/manifest.json`)
- **Git** (for cloning the repository)

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/devanshig01/RaceTrack-AI-Agent.git
   ```
2. Open **Unity Hub** and click **Add** → **Add project from disk**, then select the cloned folder.
3. Make sure Unity **2021.3.22f1** is installed. Unity Hub will prompt you to install it if needed.
4. Let Unity resolve packages automatically on first open.
5. Open the main scene: **Assets → Scenes → RaceTrackFZ.unity**.
6. Press **Play** to run the fuzzy logic racing agent.

## Project Structure

```
RaceTrack-AI-Agent/
├── Assets/
│   ├── Scenes/                        # Racing environments
│   │   ├── RaceTrackFZ.unity
│   │   ├── DragRaceTrackFZ.unity
│   │   ├── RaceTrackHuman.unity
│   │   └── RaceTrackNN.unity
│   ├── Scripts/                       # Core vehicle AI scripts
│   │   ├── AIVehicle.cs
│   │   ├── AIVehicle.Ext.cs
│   │   ├── AIVehicleNN.cs
│   │   ├── GameManager.cs
│   │   ├── AIEngineSoundManager.cs
│   │   ├── AIEngineSoundManagerNN.cs
│   │   └── GameAIStudentWork/         # Student implementations
│   │       ├── FuzzyVehicle.cs
│   │       ├── NNVehicle.cs
│   │       └── RacingTests/           # Automated racing tests
│   ├── ML-Agents/
│   │   └── Timers/                    # Performance & training logs
│   ├── Prefab/                        # Reusable game components
│   └── InitTestScene*.unity           # Test bootstrap scenes
├── Packages/
│   └── manifest.json                  # Unity package dependencies
├── ProjectSettings/                   # Unity project configuration
└── .vsconfig                          # Visual Studio workload config
```

## Racing Environments

| Scene | Description |
|---|---|
| **RaceTrackFZ** | Primary fuzzy logic racing scene — the default environment for development and testing. |
| **DragRaceTrackFZ** | Drag racing variant focused on straight-line speed rather than cornering. |
| **RaceTrackHuman** | Human-controlled vehicle for manual play and baseline comparison. |
| **RaceTrackNN** | Neural network agent environment for testing trained ML-Agents models. |

## Running Tests

1. In Unity, open the **Test Runner**: **Window → General → Test Runner**.
2. Tests are located in the `RacingTests` assembly (`Tests.RacingTest.TestFuzzyRace`).
3. Bootstrap scenes (`InitTestScene*.unity`) handle test initialization by loading the main racing scene in an isolated context.
4. Click **Run All** to execute the test suite.

## Acknowledgments

Developed in the class CS 7632: Game AI taught by Jeff Wilson, Georgia Tech.
