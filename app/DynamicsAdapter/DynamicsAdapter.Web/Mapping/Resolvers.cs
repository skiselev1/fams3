using AutoMapper;
using Fams3Adapter.Dynamics;
using Fams3Adapter.Dynamics.Employment;
using Fams3Adapter.Dynamics.Identifier;
using Fams3Adapter.Dynamics.OptionSets.Models;
using Fams3Adapter.Dynamics.SearchApiRequest;
using Fams3Adapter.Dynamics.SearchResponse;
using Fams3Adapter.Dynamics.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicsAdapter.Web.Mapping
{

    public class Date1Resolver : IValueResolver<PersonalInfo, DynamicsEntity, DateTime?>
    {
        public DateTime? Resolve(PersonalInfo source, DynamicsEntity destination, DateTime? destMember, ResolutionContext context)
        {
            return source.ReferenceDates?.SingleOrDefault(m => m.Index == 0)?.Value.DateTime;
        }

    }


    public class Date1LabelResolver : IValueResolver<PersonalInfo, DynamicsEntity, string>
    {
        public string Resolve(PersonalInfo source, DynamicsEntity destination, string destMember, ResolutionContext context)
        {
            return source.ReferenceDates?.SingleOrDefault(m => m.Index == 0)?.Key;
        }
    }

    public class Date2Resolver : IValueResolver<PersonalInfo, DynamicsEntity, DateTime?>
    {
        public DateTime? Resolve(PersonalInfo source, DynamicsEntity destination, DateTime? destMember, ResolutionContext context)
        {
            return source.ReferenceDates?.SingleOrDefault(m => m.Index == 1)?.Value.DateTime;
        }
    }

    public class Date2LabelResolver : IValueResolver<PersonalInfo, DynamicsEntity, string>
    {
        public string Resolve(PersonalInfo source, DynamicsEntity destination, string destMember, ResolutionContext context)
        {
            return source.ReferenceDates?.SingleOrDefault(m => m.Index == 1)?.Key;
        }
    }

    public class ReferenceDatesResolver : IValueResolver<DynamicsEntity, PersonalInfo, ICollection<ReferenceDate>>
    {
        public ICollection<ReferenceDate> Resolve(DynamicsEntity source, PersonalInfo destination, ICollection<ReferenceDate> referenceDates, ResolutionContext context)
        {
            List<ReferenceDate> dates = new List<ReferenceDate>();
            if (source.Date1 != null)
            {
                ReferenceDate referenceDate = new ReferenceDate
                {
                    Index = 0,
                    Key = source.Date1Label,
                    Value = new DateTimeOffset((DateTime)source.Date1)
                };
                dates.Add(referenceDate);
            }
            if (source.Date2 != null)
            {
                ReferenceDate referenceDate = new ReferenceDate
                {
                    Index = 1,
                    Key = source.Date2Label,
                    Value = new DateTimeOffset((DateTime)source.Date2)
                };
                dates.Add(referenceDate);
            }
            return dates;
        }
    }

    public class PersonReferenceDatesResolver : IValueResolver<SSG_SearchRequestResponse, Person, ICollection<ReferenceDate>>
    {
        public ICollection<ReferenceDate> Resolve(SSG_SearchRequestResponse source, Person destination, ICollection<ReferenceDate> referenceDates, ResolutionContext context)
        {
            List<ReferenceDate> dates = new List<ReferenceDate>();
            if (source.SSG_Persons[0].Date1 != null)
            {
                ReferenceDate referenceDate = new ReferenceDate
                {
                    Index = 0,
                    Key = source.SSG_Persons[0].Date1Label,
                    Value = new DateTimeOffset((DateTime)source.SSG_Persons[0].Date1)
                };
                dates.Add(referenceDate);
            }
            if (source.SSG_Persons[0].Date2 != null)
            {
                ReferenceDate referenceDate = new ReferenceDate
                {
                    Index = 1,
                    Key = source.SSG_Persons[0].Date2Label,
                    Value = new DateTimeOffset((DateTime)source.SSG_Persons[0].Date2)
                };
                dates.Add(referenceDate);
            }
            return dates;
        }
    }

    public class PersonalIdentifier_ReferenceDateResolver : IValueResolver<IdentifierEntity, PersonalIdentifier, ICollection<ReferenceDate>>
    {
        public ICollection<ReferenceDate> Resolve(IdentifierEntity source, PersonalIdentifier destination, ICollection<ReferenceDate> destMember, ResolutionContext context)
        {
            List<ReferenceDate> referDates = new List<ReferenceDate>();
            if (source.Date1 != null)
            {
                referDates.Add(new ReferenceDate() { Index = 0, Key = source.Date1Label, Value = new DateTimeOffset((DateTime)source.Date1) });
            }
            if (source.Date2 != null)
            {
                referDates.Add(new ReferenceDate() { Index = 1, Key = source.Date2Label, Value = new DateTimeOffset((DateTime)source.Date2) });
            }
            return referDates;
        }
    }

    public class NamesResolver : IValueResolver<SSG_SearchApiRequest, PersonSearchRequest, ICollection<Name>>
    {
        public ICollection<Name> Resolve(SSG_SearchApiRequest source, PersonSearchRequest destination, ICollection<Name> destMember, ResolutionContext context)
        {
            if (source?.SearchRequest?.ApplicantFirstName != null || source?.SearchRequest?.ApplicantLastName != null)
                return new List<Name>() { new Name { FirstName = source?.SearchRequest?.ApplicantFirstName, LastName = source?.SearchRequest?.ApplicantLastName, Owner = OwnerType.Applicant } };
            else
                return null;
        }
    }


    public class EmployerPhoneResponseResolver : IValueResolver<SSG_Employment, Employer, ICollection<Phone>>
    {
        public ICollection<Phone> Resolve(SSG_Employment source, Employer destination, ICollection<Phone> destMember, ResolutionContext context)
        {
            List<Phone> phones = new List<Phone>();
            if (!string.IsNullOrEmpty(source?.PrimaryPhoneNumber))
                phones.Add(new Phone { PhoneNumber = source.PrimaryPhoneNumber, Extension = source.PrimaryPhoneExtension, Type = "primaryPhone" });

            if (!string.IsNullOrEmpty(source?.PrimaryFax))
                phones.Add(new Phone { PhoneNumber = source.PrimaryFax, Type = "primaryFax" });

            if (!string.IsNullOrEmpty(source?.PrimaryContactPhone))
                phones.Add(new Phone { PhoneNumber = source.PrimaryContactPhone, Extension = source.PrimaryContactPhoneExt, Type = "primaryContactPhone" });

            foreach (SSG_EmploymentContact contact in source.SSG_EmploymentContacts)
            {
                if (!string.IsNullOrEmpty(contact.PhoneNumber))
                {
                    phones.Add(new Phone
                    {
                        ContactName = contact.ContactName,
                        PhoneNumber = contact.PhoneNumber,
                        Extension = contact.PhoneExtension,
                        Description = contact.Description,
                        Type = contact.PhoneType == null ? null : Enumeration.GetAll<TelephoneNumberType>().SingleOrDefault(m => m.Value == contact.PhoneType.Value)?.Name
                    });
                }
                if (!string.IsNullOrEmpty(contact.FaxNumber))
                {
                    phones.Add(new Phone
                    {
                        ContactName = contact.ContactName,
                        PhoneNumber = contact.FaxNumber,
                        Description = contact.Description,
                        Type = contact.PhoneType == null ? "Fax" : Enumeration.GetAll<TelephoneNumberType>().SingleOrDefault(m => m.Value == contact.PhoneType.Value)?.Name
                    });
                }
            }

            return phones;
        }
    }


    public class EmployerAddressResponseResolver : IValueResolver<SSG_Employment, Employer, Address>
    {
        public Address Resolve(SSG_Employment source, Employer destination, Address destMember, ResolutionContext context)
        {
            Address addr = new Address
            {
                AddressLine1 = source.AddressLine1,
                AddressLine2 = source.AddressLine2,
                AddressLine3 = source.AddressLine3,
                City = source.City,
                StateProvince = source.CountrySubdivisionText,
                CountryRegion = source.CountryText,
                ZipPostalCode = source.PostalCode
            };
            return addr;
        }
    }
}
