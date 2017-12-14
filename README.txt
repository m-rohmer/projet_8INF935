8INF935 - Math�matiques et physique pour le jeu vid�o
-----------------------------------------------------
Automne 2017
------------
Projet final : "Simulation d'un billard"
----------------------------------------
Thomas STIEVENARD et Martin ROHMER
----------------------------------


Le projet se lance en chargeant la sc�ne "lvl1" dans Unity,
se trouvant en :

	./Assets/scenes/lvl1


Description du projet :
-----------------------
	Pour ce projet, nous avons choisi d'utiliser Unity et son environnement
	pour programmer certains principes vus en cours ce dernier semestre.

	Les op�rations translate et rotate ont �t� impl�ment�es, la gestion de la position,
	de la vitesse et de l'acc�l�ration des objets �galement.

	Des forces peuvent �galement �tre subies par nos objets, ce qui influencera
	leur acc�l�ration et donc leur vitesse et position.
	Parmi ces forces on retrouve par d�faut la gravit� et la friction cin�tique,
	mais n'importe quelle autre force peut �tre ajout�e facilement.

	La gestion des collisions est aussi pr�sente pour des cas simples :
	le choc entre deux objets va leur donner une impulsion.

	Nous avons choisi une simulation de billard pour mettre en avant les diff�rents
	principes �voqu�s ci-dessus :
		- translation des boules sur un plan
		- gestion des collisions entre boules et avec les murs et le sol
		- forces de frictions (avec le tapis) et de gravit� impl�ment�es

	Pour cette simulation, une impulsion al�atoire est donn�e � la boule blanche
	d�s que toutes les boules ont fini de bouger.


Remarques :
-----------
	Afin de montrer le fonctionnement de la gravit�, l'une des boules est initialis�e
	au dessus du sol. Au d�marrage du jeu, cette boule va subir la gravit�.
	Lors de son contact avec le sol, elle va alors effectuer des rebonds
	jusqu'� ce que sa vitesse devienne trop faible.

	Un cube est pr�sent dans la simulation ; il s'agit simplement pour nous d'un moyen
	de montrer l'application du syst�me de rotation vu en cours. Ce cube est compos� de
	27 petits cubes, et le tout tourne correctement si l'on consid�re les petits cubes
	comme des points.
	Si le tout rotate bien, les cubes pris un � un ne tournaient pas sur eux m�me pour autant.
	Nous avons essay� de corriger cela en leur influant �galement une rotation, mais
	il y a un d�calage avec la rotation globale faisant que le cube principal (compos�
	des 27 plus petits) n'a pas toujours une allure de cube pour autant.


A propos du code :
------------------
	Un GameManager est l� pour r�pertorier tous les objets pr�sents dans le jeu, et les
	actualiser � chaque frame en appelant leur update respectif.
	C'est �galemet lui qui va d�tecter les collisions, et insuffler une impulsion aux objets
	en collision.

	Les objets de notre jeu proviennent de la classe abstraite Item, qui regroupe tous les
	attributs qu'on peut associer � nos objets (nom, poids, tableau des forces subises etc.).

	La classe MovingItem h�rite de Item et correspond aux objets pouvant se d�placer. Nos
	boules de billiard en descendent, depuis la classe Ball.
	MovingItem va g�rer le calcul et l'actualisation de la position, vitesse et acc�l�ration
	de l'objet. Pour cela il prend aussi en compte les forces qui s'appliquent � l'objet,
	en les calculant / ajoutant / supprimant selon l'interaction de l'objet avec son
	environnement.
	Il va par exemple d�tecter si l'objet est au sol, et si oui s'il est dans un cas de rebond.
	S'il y a rebond, une force va �tre appliqu�e � l'objet le faisant rebondir.
	S'il n'y a pas rebond, on enl�ve la gravit� de l'objet (plus optimal que de lui rajouter
	une force compensant la gravit�, pour �viter � l'objet de s'enfoncer dans le sol).

	On retrouve enfin les classes repr�sentant les murs et le sol : des items, sans caract�ristiques
	particuli�res.
	Et aussi les classes s'occupant de la formation et de la rotation du cube et de ses �l�ments.