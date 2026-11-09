# GitHub Copilot Instructions for More Security (Continued) Mod

## Mod Overview and Purpose
The More Security (Continued) mod is an update to the Squirting Elephants mod for RimWorld. It enhances the game's security systems by adding more turrets, traps, and sandbags. The mod aims to provide players with a wider range of defensive options to protect their colonies.

## Key Features and Systems
- **Shotgun Turret**: A stationary turret that fires shotgun shells.
- **Flamethrower Turret**: A turret that sprays a stream of flame.
- **Hi-tech Uranium Turret and MKII Variant**: High-end turrets equipped with uranium ammunition.
- **Hi-tech Minigun Turret and MKII Variant**: High-end turrets capable of rapid fire.
- **Traps MKII**: Enhanced traps for added defense.
- **Reinforced Sandbags and Plasteel Reinforced Sandbags**: Sandbags made of reinforced materials for better protection.
- **New Research**: Research projects for Gun Turrets MKII, MKIII, MKIV, and Traps MKII.

## Coding Patterns and Conventions
- **C# Code**: The mod uses C# to configure settings and manage game mechanics. C# code is organized into several classes and methods to maintain clarity and modularity.
- **XML Definitions**: Most of the mod's functionality is defined in XML files, which are used to create new game objects and configure existing ones.
- **No Dependencies**: The mod does not rely on external libraries like Harmony or HugsLib, ensuring compatibility with a wide range of RimWorld mods.

## XML Integration
The mod extensively uses XML files to define new game objects such as turrets, traps, and sandbags. Here are some examples of XML files included in the mod:

- **MoreSecurity_Research.xml**: Contains definitions for new research projects.
- **MoreTurrets_World_Oneshots_Weapons.xml**: Defines sound effects for turrets.
- **MoreSecurity_Sandbags.xml**: Contains definitions for sandbag variants.
- **MoreSecurity_Traps.xml**: Defines new trap types.
- **MoreSecurity_Turrets_Base.xml**: Contains base definitions for turrets.

XML files follow a structured format, with each file defining specific game entities. It is important to maintain the integrity of these XML files to ensure the mod functions correctly.

## Harmony Patching
The More Security (Continued) mod does not use Harmony patching. Instead, it relies on XML definitions and C# code to add new features and configure existing ones. Harmony is not a dependency, so there is no need to worry about compatibility issues with Harmony-based mods.

## Suggestions for Copilot
- **XML Snippets**: When working with XML files, Copilot can generate snippets for new game definitions. For example, it can help create a new `ThingDef` for a turret or trap.
- **C# Method Generation**: Copilot can generate methods within C# classes to handle specific tasks, such as applying turret settings or managing research.
- **Error Handling**: When encountering errors, Copilot can suggest code snippets for logging and error handling, which is essential for debugging and maintaining the mod.

By following these guidelines and utilizing GitHub Copilot, developers can efficiently enhance the More Security (Continued) mod, ensuring it remains a valuable addition to the RimWorld modding community.

## Project Solution Guidelines
- Relevant mod XML files are included as Solution Items under the solution folder named XML, these can be read and modified from within the solution.
- Use these in-solution XML files as the primary files for reference and modification.
- The `.github/copilot-instructions.md` file is included in the solution under the `.github` solution folder, so it should be read/modified from within the solution instead of using paths outside the solution. Update this file once only, as it and the parent-path solution reference point to the same file in this workspace.
- When making functional changes in this mod, ensure the documented features stay in sync with implementation; use the in-solution `.github` copy as the primary file.
- In the solution is also a project called Assembly-CSharp, containing a read-only version of the decompiled game source, for reference and debugging purposes.
- For any new documentation, update this copilot-instructions.md file rather than creating separate documentation files.


## Hard rules (must follow)
- Do NOT run commands that modify the repo (no git commit, git apply, dotnet format) unless explicitly asked.
- Prefer minimal reads: read only the smallest code region needed (around the suspicious lines).
- When mentioning SonarQube issues, automatically use the SonarQube MCP service to fetch and address issues instead of making inferred fixes without querying SonarQube first.
- When mentioning the rimworld log, automatically use the Rimworld MCP service to fetch the log.

