The goal of this project is to build a developer tool to create 2d levels in the Unity engine, these levels will be creatable in engine, and can be saved or loaded by file
# Objectives
- The algorithm should be able to take a created file, and generate a number of rigid bodies representing the terrain
- Objects that are split from each other should be turned into their own rigid bodies, with certain objects splitting off being converted into dynamic rigid bodies
- This terrain should be destructible during gameplay, without changing the original file
- The developer should be able to mark areas as solid, and some areas as destructible, any rigid bodies with no solid area will become dynamic
