CREATE TABLE Trait (
                       id INTEGER PRIMARY KEY AUTOINCREMENT,
                       name TEXT NOT NULL UNIQUE,
                       type VARCHAR(20) NOT NULL,
                       designer_cost INTEGER NOT NULL,
                       minimum_age INTEGER,
                       maximum_age INTEGER
);

CREATE TABLE SkillModifier (
                               id INTEGER PRIMARY KEY AUTOINCREMENT,
                               trait_id INTEGER NOT NULL,
                               skill VARCHAR(20) NOT NULL,
                               modifier INTEGER NOT NULL,
                               FOREIGN KEY (trait_id) REFERENCES Traits(id) ON UPDATE CASCADE ON DELETE CASCADE,
                               UNIQUE(trait_id, skill) -- Ensure unique combination of trait_id and skill
);

CREATE TABLE NonApplicableTrait (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    trait_id INTEGER NOT NULL,
                                    opposite_trait_id INTEGER NOT NULL,
                                    FOREIGN KEY (trait_id) REFERENCES Traits(id) ON UPDATE CASCADE ON DELETE CASCADE,
                                    FOREIGN KEY (opposite_trait_id) REFERENCES Traits(id) ON UPDATE CASCADE ON DELETE CASCADE,
                                    UNIQUE(trait_id, opposite_trait_id) -- Ensure unique combination of trait_id and opposite_trait_id
);
