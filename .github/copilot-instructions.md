# .github/copilot-instructions.md

## RimWorld Mod Overview and Purpose

The "More Security" mod is designed to enhance the security mechanisms within RimWorld, a popular survival and strategy game. This mod introduces new security-related features and settings aimed at improving gameplay dynamics, particularly in defense and tactical strategy.

### Key Features and Systems

- **Enhanced Security Settings**: Customizable security settings allow players to tailor their defense parameters.
- **User Interface Improvements**: Enhanced table entities facilitate an intuitive UI for security management and configuration.
- **Dynamic Updates**: The system supports real-time updates to security components, enhancing gameplay responsiveness.
  
### Coding Patterns and Conventions

- **Naming Conventions**: Follow PascalCase for class names and method names, use camelCase for method parameters and local variables.
- **Class Design**: Utilize static classes (`Utils`, `Extensions`) for utility functions, implement data management through specific data classes (`TableData`, `TurretSettings`).
- **Method Structure**: Methods should be cohesive in function, and decoupled for maintainability and clarity. Public methods should be adequately documented.

### XML Integration

- The project integrates XML for data serialization and mod settings saving/loading, supported by classes like `MoreSecuritySettings` and `MS_SettingsData`.
- Ensure XML files conform to the required schema for seamless data integration. Use proper serialization attributes to facilitate the process.

### Harmony Patching

- **Harmony Library**: The mod leverages the Harmony library to patch existing game methods. This allows for modifications without altering the original game files.
- **Patching Strategy**: Apply patches judiciously to ensure compatibility and prevent unintended interactions with other mods. Patches should be targeted towards specific game methods, focusing on security enhancements.

### Suggestions for Copilot Usage

To maximize the use of GitHub Copilot in the development of this mod:

1. **XML Parsing**: Utilize Copilot to suggest XML structures, ensuring they conform to the expected data formats used within RimWorld.
2. **Harmony Management**: Generate boilerplate code for Harmony patches using Copilot, focusing on method patches relevant to the mod’s enhancement goals.
3. **UI Components**: Leverage Copilot's assistance in generating and refining methods that manage the user interface and table dynamics.
4. **Data Structure**: Use Copilot for guidance on implementing efficient data management patterns within classes like `TableData` and `TurretSettings`.

By adhering to these guidelines and thoroughly utilizing GitHub Copilot, you can enhance the development workflow and ensure high-quality code for the "More Security" RimWorld mod.
