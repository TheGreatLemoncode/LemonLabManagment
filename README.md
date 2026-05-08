# LemonLabManagment
projet prog de yao pascal joseph

## Introduction
L.L.M est une application programmé et développé par yao pascal joseph. 
Il s'agit d'une application de gestion de matériaux infomratique telsque
- ordinateur
- imprimante
- serveur
- etc
  
Elle est conçu pour un public de débutants et intermédiaires qui se lancent
dans la création de "homeLab" informatique. L'application permet de :
- enregistrer différent type de machines pour les afficher dans l'interface.
- changer le status d'une machine pour reflêter son utilisation réel
- créer une organisation / rejoindre une organisation afin de partager les ressources avec d'autres utilisateurs
- créer une répresentation graphique de système de communication entre machines

## Degré de complétion
Au stade de la release, l'application se montre incomplet en raison de l'absence d'une fonctionnalité.
Cette dernière consistait à enregistrer les répresentation graphique afin de pouvoir les afficher plus tard.
### Bugs
Le projet contient également quelques bugs persistant telsque :
- le champ "courriel" dans la page de connexion qui s'Affiche toujours rouge
- la vidéo d'introduction qui ne contient pas d'audio
- les objets qui n'apparaisent plus au centre du canvas pendant la création de circuit

## Technologies utilisés
L'application est conçu entierement en wpf/c#, ce qui la rend optimal
pour un environnement windows.

## Piste d'amélioration
Pour améliorer ce projet j'aurais pu faire en sorte que l'application fasse en sort que l'utilisateur 
vois les membres de son organisation et qu'il puisse supprimer des machines.

## Procédure d'installation client
L'installation doit se faire sur un appareil ayant dotnet 8.0 et plus (windows de préférence).
Pour installer l'application, vous devez télécharger depuis la fenêtre release LLM_SetUp.exe et LLM_SetUp.msi.
une fois tous les deux téléchargé et dans le même dossier, vous pouvez exécuter LLM_SetUp.exe. À cette étape
vous pouvez juste suivre les instructions de l'installateur.

## Procédure d'installation développeur
Dans le dossier release, vous devez télécharger le fichier code source. Un fois téléchargé et extrait dans le dossier voulu, vous pouvez ouvrir la 
solution à l'aide de visual studio. Une fois la solution chargé vous aurez besoin d'avoir dotnet 8.0 ou plus d'installer sur votre machine. Finalement 
vous aurez besoin de télécharger les packages nuget "toastnotifier". Après tous ces étapes vous aurez accèss au code source du projet.

## Dossier de données
Toutes les données de l'application produit par l'utilisateur sont dans le dossier (après avoir exécuter l'application):
C:\ProgramData\LemonLabManagment

