using Wetr.Server.BL.IDefinition;
using Wetr.Server.DAL.DTO;

namespace Wetr.Cockpit.ViewModels
{
    public class CommunityVM : ViewModelBase
    {
        private Community community;
        private IMasterdataManager masterDataManager;
        private ManageDataVM parent;

        public CommunityVM(Community community, IMasterdataManager masterDataManager, ManageDataVM parent)
        {
            this.community = community;
            this.masterDataManager = masterDataManager;
            this.parent = parent;
        }

        public int ID
        {
            get
            {
                return this.community.ID;
            }
            set
            {
                if(this.community.ID != value)
                {
                    this.community.ID = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public int PostalCode
        {
            get
            {
                return this.community.PostalCode;
            }
            set
            {
                if(this.community.PostalCode != value)
                {
                    this.community.PostalCode = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public string Name
        {
            get
            {
                return this.community.Name;
            }
            set
            {
                if (this.community.Name != value)
                {
                    this.community.Name = value;
                    this.RaisePropertyChanged();
                }
            }
        }
        public int DistrictID
        {
            get
            {
                return this.community.DistrictID;
            }
            set
            {
                if (this.community.DistrictID != value)
                {
                    this.community.DistrictID = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}