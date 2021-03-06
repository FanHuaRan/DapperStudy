﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DapperStudyUnitTest
{
    [DataContract(Namespace = "www.ranran.MusicStoreWcfRest")]
    public class Artist
    {
        [DataMember]
        [ScaffoldColumn(false)]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "An Artist Name is required")]
        [DataMember]
        public string Name { get; set; }
    }
}