# AI for Software Development Day 1 Exercise

## Learning Objectives

- Learn about LLMs and other AI tools
- Discover the implications and limitations of using AI to develop code including Security and Compliance
- Learn how to get started with GitHub Copilot in VS Code
- Use Copilot when working on JavaScript Projects

### Tasks

Look at some of the vanilla JavaScript Exercises we did previously (Scrabble Challenge, Poker Challenge, Softplay etc) you're going to use Copilot to solve some of these. You can approach this in a variety of ways so that you get a greater feel for what Copilot is capable of and where it struggles.

Begin by making a Fork as usual for this repo, as you complete solutions for each of the JavaScript Exercises you're going to play with, put links to the updated exercises (in your GitHub Account) in here and at the end of the session push this up to GitHub and make a Pull Request with it. That way we'll be able to easily find the repos you've worked on.

Try and solve each Exercise using Copilot as much as possible, ie keep you manual interventions to a minimum. If you can keep track of how much of each exercise is done by Copilot and how much is your manual interventions to get them to work.

Do not use your previous forks for these Exercises if they exist, instead create new forks with the letters `AI` appended to the project name, so that we can filter them out later on. Do not create PRs for these Exercises.

Your tutor should be able to give you a list of some of the suitable JavaScript Exercises to use, if you can't find any, if you're not sure then please ask.

### Links to My JavaScript Exercises Solved Using Copilot

- https://github.com/shyye/js-fizzbuzz-procedural-ai
- https://github.com/shyye/js-dom-greengrocers-ai
-

Setup steps (personal notes):  
1. Create a new repo, e.g. `original-name-ai`.
2. Clone the original repo and name it the same as the new repo.
```
git clone `<SSH TO REPO>` `<new repo name, e.g. original-name-ai>`
```
1. Open git bash terminal in the folder for the cloned repo.  
   Remove remote origin:  
```
git remote remove origin
```
1. Add new origin / upstream to the newly created repo named some-original-name-ai:  
```
git remote add origin <SSH TO REPO>
git branch -M main
git push -u origin main
```
