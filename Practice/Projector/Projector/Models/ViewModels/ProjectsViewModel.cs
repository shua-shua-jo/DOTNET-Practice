﻿using Projector.Models.Helpers;
using Projector.Models.OutputModels;

namespace Projector.Models.ViewModels
{
    public class ProjectsViewModel
    {
        public List<ProjectItemDTO> Projects { get; set; }

        private ProjectsViewModel(List<ProjectItemDTO> projects)
        {
            Projects = projects;
        }

        public static ProjectsViewModel FromDTO(List<ProjectItemDTO> projects)
        {
            return new ProjectsViewModel(projects ?? new List<ProjectItemDTO>());
        }
    }
}
