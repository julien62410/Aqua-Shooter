# Aqua-Shooter

Features :

    Les requins sont très rapides.

    Les poulpes envoi de l'encre non mortel mais très handicapante

    Les rochers sont des obstacles indestructibles

    Le sous-marin peut se déplacer de haut en bas

    Les torpilles du joueurs sont lancées depuis la position du sous-marin
    
    
    
    La grandes tache d'encre possède un délai durant lequel elle est affichée. Le délai se réincrémente à chaque fois que l'on se prend un jet d'encre

    Les torpilles tues les requins et les poulpes puis sont détruites. Elles sont détruites à la rencontre des rochers. Elles n'intéragissent pas avec les jet d'encre
    
    Tous les objets sont remis dans le stock lorsqu'ils sortent de l'écran sur l'axe X
    
    Le sous-marin est arrété sur l'axe Y aux bords de l'écran
    
    
    
    Les quetes sont faites uniquement pour le challenge. Elles sont afficher entre chaque levels. Si la quètes courantes est fini à la fin du level une nouvelle sera proposé, sinon notre avancement sera afficher
    
    Le score est affiché à chaque fin de partie et ajouté aux HighScores s'il en fait parties. Ceux-ci sont conservé via une BD sqlite ou l'on conserve les 10 meilleurs scores.
    
Tools :

    Le GM de la scène Play à son inspector refait :
    
        ReorderList pour les différents niveaux qui se succèdes dans le jeu
        
        Script pour le fichier des quetes
        
        Slider pour les valeurs numériques
        
        Transform pour les contours de l'écran et la position du sous-marin avec leur valeur numérique afficher
        
    Asset --> Create --> Custom :
    
        On peut générer l'ensemble des quètes sous la forme d'un Asset via un fichier csv (qui sera ensuite à passer au GM_Play)
        
        On peut générer l'ensemble des levels sous la forme d'Asset via des fichiers csv (qui seront ensuite à passer au GM_Play)
        
    Les différents levels sont ouvrable dans une window via leur fichier. Cela permet de mieu visualiser sa scène et aussi d'y apporter des modifications (qui ne sont pas répercuté dans le csv !!!)
