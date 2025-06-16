
---
# RpnCalculator

A desktop Reverse Polish Notation (RPN) calculator application built with C# and Avalonia UI.

## Features

- Intuitive graphical user interface for RPN calculations
- Supports addition, subtraction, multiplication, and division
- Enter numbers and operators using buttons or keyboard (numpad and main keys supported)
- Stack operations: swap, clear, and remove
- Error messages displayed in-place for invalid operations
- Graph feature for visualizing stack data (expandable for future features)
- Dark theme, modern UI with Fluent styling

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Avalonia UI](https://avaloniaui.net/) (handled via NuGet)

### Build and Run

1. Clone the repository:
   ```sh
   git clone https://github.com/JBrunnerhtl/RpnCalculator.git
   cd RpnCalculator/avaloniarpncalculator/RpnCalcApp
   ```

2. Restore dependencies and build:
   ```sh
   dotnet restore
   dotnet build
   ```

3. Run the application:
   ```sh
   dotnet run
   ```

## Usage

- Use the on-screen buttons or your keyboard to enter numbers and select operations.
- Press `Enter` (or the "Enter" button) to push a value to the stack.
- Use operators (`+`, `-`, `*`, `/`) to perform calculations on the stack's topmost values.
- Additional controls: "Swap" switches the top two elements, "Clear" empties the stack, "Remove" deletes the last digit or value.
- Activate the "Graph" feature to visualize the stack (if available).

## Keyboard Shortcuts

- `Enter`: Push current input to the stack
- `+`, `-`, `*`, `/`: Perform operation
- `S`: Swap
- `C`: Clear
- `Backspace`: Remove digit
- `Esc`: Exit application

## Contributing

Contributions are welcome! Feel free to open issues or submit pull requests.

## License

This project is licensed under the MIT License.

## Error

Little error with popup after you clicked the checkbox if you have an invilad graph input

---

Let me know if you want it to include additional sections (e.g., screenshots, planned features, localization) or if you want the README in a different format!
