# ElevatorChallengeNew
| Configuration/
└── ElevatorSettings.cs #
Configuration model for elevator settings 
├── Entities/ 
├── Building.cs # Building entity with floors and waiting passengers
├── Constants.cs # Application constants and default values 
└── Enums.cs 
├── Interfaces/ 
├── IBuilding.cs # Building interface
├── IElevator.cs # Elevator interface └
── IElevatorController.cs # Controller interface
├── Models/
├── Elevator.cs # Elevator model implementation
└── ElevatorFac.cs # Factory for creating different elevator types
├── Services/ └── ElevatorController.cs # Main controller service 
├── Helper/ 
└── ConsoleHelper.cs # Console output utilities with colors
├── Validators/ ├── ElevatorRequest.cs # Request model for validation
└── ElevatorRequestValidator.cs # FluentValidation rules 
├── appsettings.json # Application configuration
├── ElevatorSimulation.csproj # Project file with dependencies 
└── Program.cs # Main application entry point Prerequisites 
.Net8*
