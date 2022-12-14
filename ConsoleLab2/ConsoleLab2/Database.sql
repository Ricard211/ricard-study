CREATE TABLE Consoles (Id serial,
					   Name varchar(300),
					   Creator varchar(100), 
					   Year Int,
					   Units varchar(30),
					   Generation Int
					  );
						
						
insert into Consoles(Name, Creator, Year, Units, Generation) values 
('Sega Genesis', 'Sega Corporations', 1988, '30 mln', 2),
('Playstation 5', 'Sony Interactive Entertainment', 2020, '25 mln', 9),
('Xbox 360', 'Microsoft', 2005, '84 mln', 7),
('Playstation', 'Sony Interactive Entertainment', 1994, '102 mln', 5);


select * from Consoles;


CREATE TABLE ConsoleGames (Id serial,
						Name varchar(300),
						  Developer varchar(100),
						  Year int,
						   Units varchar(100),
						  Compatability varchar(100)
						  );
						  
insert into ConsoleGames (Name, Developer, Year, Units, Compatability) values	
('The last of Us', 'Naughty Dog', 2013, '17 mln', 'Playstation'),
('Forza Horizon 5', 'Playground Games', 2021, '15 mln', 'Xbox'),
('Pokemon Stadium 2', 'Nintendo EAD', 2000, '2.5 mln', 'Nintendo'),
('Horizon Forbidden West', 'Guerilla Games', 2022, '30 mln', 'Playstation');

select * from ConsoleGames;