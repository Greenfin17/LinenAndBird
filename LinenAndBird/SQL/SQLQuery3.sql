select * 
                        from Orders O
                        join birds B
                            on B.Id = O.BirdId
                        join Hats H
                            on H.Id = O.HatId
                        where O.Id = @id;