using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data
{
    public class Entity
    {
        public string Id { get; set; }
        
        /// <summary>
        /// 全局唯一身份
        /// </summary>
        public string IdentityId { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public string TenantId{ get; set; }

        /// <summary>
        /// 租户下的UserId
        /// </summary>
        public string UserId { get; set; }


        

        /// <summary>
        /// 创建人对应的UserId
        /// </summary>
        public string CreatedBy { get; set; }
        public string LastUpdatedBy { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime LastUpdatedAt { get; set; }



    }
}
