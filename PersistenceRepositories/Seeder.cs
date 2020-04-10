using Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersistenceRepositories
{
    class Seeder
    {
        public RoleRepository roleRepository { get; set; }
        public UserRepository userRepository { get; set; }

        public void SeedRoles()
        {
            List<Role> roles = new List<Role>();
            roles.Add(new Role() { Name = "Administrador" });
            roles.Add(new Role() { Name = "Auxiliar" });

            foreach(Role role in roles)
            {
                roleRepository.Save(role);
            }
        }

        public void Seed()
        {
            SeedRoles();
        }
    }
}
