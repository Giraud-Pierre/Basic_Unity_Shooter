# Explication du projet
L'objectif de ce projet est de créer un first-person shooter rudimentaire.

Le joueur est représenté par un gameobject "PlayerBody" : un pavé droit bleu qui apparait sur une plateforme verte "safezone".
La caméra est fixée en temps qu'enfant du PlayerBody, à 0.4 en haut du centre du PlayerBody (la vue d'une personne normale se situant au niveau de la tête, en haut du corps). (Cela permet de gérer plus efficacement les mouvements de la caméra et, en déliant la caméra et le playerbody et en détruisant le playerbody dans le code à la mort du joueur, de permettre au joueur d'observer les alentours de sa mort en l'empêchant de bouger *(ou éventuellement de mettre un mode de déplacement alternatif au joueur mort (possibilité de voler, passer à travers les murs) mais je ne l'ai pas implémenté ici)* ). 

Les mouvements de rotation de la camera sont permis par le script "CameraController" lié à la caméra.
Les mouvements de translation du playerBody (et donc de la caméra), ainsi que la gestion des points de vie du joueur, le saut et le tir de balles sont permis par le script "PlayerController" qui est lié au PlayerBody.
Un autre script est lié au PlayerBody: le script "EnnemySpawner". Celui-ci permet le spawn de clones du gameobject prefab Ennemi (un pavé droit rouge) lorsque le joueur entre sur le gameobject "terrain" où va se dérouler la partie (et sort donc de la SafeZone).

Comme mentionné ci-dessus, le joueur peut générer des clones d'un gameobject prefab "bullet" grâce au script "PlayerController" qui génère le clone dans la bonne direction et lui donne une force initiale ("propulsion" de la balle). 
A cette balle ("bullet") est lié un script "BullerController" qui permet de détruire la balle lorsqu'elle sort du terrain ou lorsqu'elle touche le sol (le terrain).

A ce gameobject prefab ennemi est lié un script "EnnemiController" qui gère les points de vie de chaque clone ennemi en question et son déplacement vers le joueur.

# Comment Jouer ?
Le joueur peut se déplacer grâce aux flèches directionnelles ou au touches ZQSD (Z : en avant, S: en arrière, Q: à gauche, D: à droite), déplacer sa caméra avec la souris (ou le pavé tactile de son ordinateur) et sauter avec la barre espace.

Il démarre sur une zone verte, la zone safe dans laquelle il est en "relative" sureté.
Quand il sort de la zone safe et entre sur le terrain à côté, la zone safe disparait et des ennemis apparaissent sur une position aléatoire du terrain toutes les 10 secondes.

Les ennemis se déplace vers le joueur à une vitesse de base d'un peu moins de la moitié de celle du joueur.
Quand l'ennemi touche le joueur, celui-ci est repoussé en arrière et perd 1 point de vie.

Le joueur peut se défendre en tirant  des balles avec le bouton gauche de la souris (ou du pavé tactile) ou avec le bouton ctr gauche.
Un ennemi touché par une balle est repoussé en arrière et perd un points de vie (le joueur peut aussi se toucher avec ses propres balles, en tirant vers le haut par exemple).

Chaque ennemi démarre avec 3 points de vie et meurt quand ses points de vie arrive à 0 ou tombe en dehors de la plateforme.
Le joueur démarre avec 10 points de vie et meurt quand ses points de vie arrive à 0 ou tombe en dehors de la plateforme, auquel cas son corps disparait mais la caméra reste, permettant au joueur s'observer les alentours de sa mort mais pas de bouger. La console affiche alors un message signalant que le joueur a été tué.
