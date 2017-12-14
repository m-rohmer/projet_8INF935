8INF935 - Mathématiques et physique pour le jeu vidéo
-----------------------------------------------------
Automne 2017
------------
Projet final : "Simulation d'un billard"
----------------------------------------
Thomas STIEVENARD et Martin ROHMER
----------------------------------


Le projet se lance en chargeant la scène "lvl1" dans Unity,
se trouvant en :

	./Assets/scenes/lvl1


Description du projet :
-----------------------
	Pour ce projet, nous avons choisi d'utiliser Unity et son environnement
	pour programmer certains principes vus en cours ce dernier semestre.

	Les opérations translate et rotate ont été implémentées, la gestion de la position,
	de la vitesse et de l'accélération des objets également.

	Des forces peuvent également être subies par nos objets, ce qui influencera
	leur accélération et donc leur vitesse et position.
	Parmi ces forces on retrouve par défaut la gravité et la friction cinétique,
	mais n'importe quelle autre force peut être ajoutée facilement.

	La gestion des collisions est aussi présente pour des cas simples :
	le choc entre deux objets va leur donner une impulsion.

	Nous avons choisi une simulation de billard pour mettre en avant les différents
	principes évoqués ci-dessus :
		- translation des boules sur un plan
		- gestion des collisions entre boules et avec les murs et le sol
		- forces de frictions (avec le tapis) et de gravité implémentées

	Pour cette simulation, une impulsion aléatoire est donnée à la boule blanche
	dès que toutes les boules ont fini de bouger.


Remarques :
-----------
	Afin de montrer le fonctionnement de la gravité, l'une des boules est initialisée
	au dessus du sol. Au démarrage du jeu, cette boule va subir la gravité.
	Lors de son contact avec le sol, elle va alors effectuer des rebonds
	jusqu'à ce que sa vitesse devienne trop faible.

	Un cube est présent dans la simulation ; il s'agit simplement pour nous d'un moyen
	de montrer l'application du système de rotation vu en cours. Ce cube est composé de
	27 petits cubes, et le tout tourne correctement si l'on considère les petits cubes
	comme des points.
	Si le tout rotate bien, les cubes pris un à un ne tournaient pas sur eux même pour autant.
	Nous avons essayé de corriger cela en leur influant également une rotation, mais
	il y a un décalage avec la rotation globale faisant que le cube principal (composé
	des 27 plus petits) n'a pas toujours une allure de cube pour autant.


A propos du code :
------------------
	Un GameManager est là pour répertorier tous les objets présents dans le jeu, et les
	actualiser à chaque frame en appelant leur update respectif.
	C'est égalemet lui qui va détecter les collisions, et insuffler une impulsion aux objets
	en collision.

	Les objets de notre jeu proviennent de la classe abstraite Item, qui regroupe tous les
	attributs qu'on peut associer à nos objets (nom, poids, tableau des forces subises etc.).

	La classe MovingItem hérite de Item et correspond aux objets pouvant se déplacer. Nos
	boules de billiard en descendent, depuis la classe Ball.
	MovingItem va gérer le calcul et l'actualisation de la position, vitesse et accélération
	de l'objet. Pour cela il prend aussi en compte les forces qui s'appliquent à l'objet,
	en les calculant / ajoutant / supprimant selon l'interaction de l'objet avec son
	environnement.
	Il va par exemple détecter si l'objet est au sol, et si oui s'il est dans un cas de rebond.
	S'il y a rebond, une force va être appliquée à l'objet le faisant rebondir.
	S'il n'y a pas rebond, on enlève la gravité de l'objet (plus optimal que de lui rajouter
	une force compensant la gravité, pour éviter à l'objet de s'enfoncer dans le sol).

	On retrouve enfin les classes représentant les murs et le sol : des items, sans caractéristiques
	particulières.
	Et aussi les classes s'occupant de la formation et de la rotation du cube et de ses éléments.