﻿using EMS.Users;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace EMS.EntityFrameworkCore;

public static class EMSEfCoreEntityExtensionMappings
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public static void Configure()
    {
        EMSGlobalFeatureConfigurator.Configure();
        EMSModuleExtensionConfigurator.Configure();

        OneTimeRunner.Run(() =>
        {
            ObjectExtensionManager.Instance
                   .MapEfCoreProperty<IdentityUser, double>(
                       nameof(AppUser.amountOwed),
                       (entityBuilder, propertyBuilder) =>
                       {
                           propertyBuilder.HasDefaultValue(0);
                           //  propertyBuilder.HasMaxLength(UserConsts.MaxTitleLength);
                       }
                   ).MapEfCoreProperty<IdentityUser, double>(
                       nameof(AppUser.amountOwes),
                       (entityBuilder, propertyBuilder) =>
                       {

                           propertyBuilder.HasDefaultValue(UserConsts.MinamountValue);
                       }
                       ).MapEfCoreProperty<IdentityUser, double>(
                       nameof(AppUser.totalAmount),
                       (entityBuilder, propertyBuilder) =>
                       {
                           propertyBuilder.HasDefaultValue(0); // propertyBuilder.HasDefaultValue(UserConsts.MinReputationValue);
                       }
                       ).MapEfCoreProperty<IdentityUser, bool>(
                       nameof(AppUser.isRegistered),
                       (entityBuilder, propertyBuilder) =>
                       {
                           propertyBuilder.HasDefaultValue(UserConsts.DefaultisRegisteredValue);
                       }
                   );
            /* You can configure extra properties for the
                 * entities defined in the modules used by your application.
                 *
                 * This class can be used to map these extra properties to table fields in the database.
                 *
                 * USE THIS CLASS ONLY TO CONFIGURE EF CORE RELATED MAPPING.
                 * USE EMSModuleExtensionConfigurator CLASS (in the Domain.Shared project)
                 * FOR A HIGH LEVEL API TO DEFINE EXTRA PROPERTIES TO ENTITIES OF THE USED MODULES
                 *
                 * Example: Map a property to a table field:

                     ObjectExtensionManager.Instance
                         .MapEfCoreProperty<IdentityUser, string>(
                             "MyProperty",
                             (entityBuilder, propertyBuilder) =>
                             {
                                 propertyBuilder.HasMaxLength(128);
                             }
                         );

                 * See the documentation for more:
                 * https://docs.abp.io/en/abp/latest/Customizing-Application-Modules-Extending-Entities
                 */
        });
    }
}
