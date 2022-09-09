# Explorus (Équipe B)

## Modèles de conception

## Singleton
Classe Map
Classe GameMaster
Il va toujours y avoir juste une seule instance de ces classes.

## Factory
Classe Map
Le factory permet de créer tous les objets de la map selon le type d'objet qui est défini en tant qu'une couleur de pixel.

## Composite
Classe CompoundGameObject
La hiérarchie permet de mettre ensemble tous les GameObjects de la map et on peut manipuler la classe CompoundGameObject comme étant un objet unique au lieu de gérer tous les différents GameObjects.

## Observer
Classe GameForm
Lorsqu'un bouton a été enfoncé, il envoie une notification a ceux qui sont abonnés pour leur informer de son état. 

## State
Classe GameEngine
Il y a 3 états dans le jeu (Resumed, Paused, End).

## Facade
Classe IGameEngine
L'interface IGameEngine permet d'offrir juste l'accès de base à l'usager pour qu'il puisse partir le jeu.