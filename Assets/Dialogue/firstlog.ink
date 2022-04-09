-> main

=== main ===
Which pokemon do you choose?
    + [Charmander]
        -> chosen("Charmander")
    + [Bulbasaur]
        -> chosen("Bulbasaur")
    +[Squirtle]
        -> chosen ("Squirtle")
        
=== chosen(pokemon) ===
You chose {pokemon}!
that's cool -> introduce

=== introduce ===
Hi,My name is Non .Nice to me you
    + [Me too.] -> Ending
    + [Yes.] -> Ending

=== Ending ===
Thank you
->END
