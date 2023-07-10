# Welcome to my Digital Jukebox Project
This repository showcases my project aimed at creating a digital jukebox. The project is designed using a microservices architecture, with each component housed in a separate folder according to its functionality. The structure helps in efficient organization and effective management.

## Project Folders:
- **📁JukeLadder-ApiGateway** : This folder contains the microservice acting as the gateway for all API requests. It routes requests to the appropriate microservice and acts as an entry point for our system.

- **📁JukeLadder-Playlist** : This microservice is implemented using clean architecture principles and MongoDB for data storage. It's responsible for the creation and management of playlists.

- **📁JukeLadder-Catalog** : This microservice is designed with clean architecture principles to manage the music catalog. It interacts with the Deezer API to provide a wide range of music choices for the users.

- **📁JukeLadder-Billing** : This microservice also follows the clean architecture principles and manages payments via Stripe. It handles all transactions related to the digital jukebox service.

- **📁JukeLadder-Frontend** : The frontend of the application is built with Nuxt.js. It handles user authentication, playlist broadcasting, and administrative functions.

## Purpose of this Repository
This repository serves as an exemplar of my abilities in developing microservice-based applications, employing modern design principles, and integrating various APIs. It's a testament to my skills in both backend and frontend development, and my proficiency in implementing modern development techniques and tools.
For any queries or suggestions, feel free to open an issue. I hope you find this project as exciting to explore as it was for me to develop!
