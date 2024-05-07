create table Traits
(
    id            INTEGER not null primary key autoincrement,
    name          TEXT    not null,
    type          INTEGER not null,
    designer_cost INTEGER not null,
    minimum_age   INTEGER,
    maximum_age   INTEGER
);
CREATE TABLE SkillModifiers
(
    TraitId  INTEGER NOT NULL REFERENCES Traits (id) ON UPDATE CASCADE ON DELETE CASCADE,
    Skill    TEXT    NOT NULL,
    Modifier INTEGER NOT NULL,
    PRIMARY KEY (TraitId, Skill)
);
create table NonApplicableTraits
(
    TraitId         INTEGER not null
        constraint NonApplicableTraits_Traits_id_fk references Traits on update cascade on delete cascade,
    OppositeTraitId INTEGER not null
        constraint NonApplicableTraits_Traits_id_fk_2 references Traits on update cascade on delete cascade,
    Id              INTEGER not null
        constraint NonApplicableTraits_pk primary key autoincrement
);