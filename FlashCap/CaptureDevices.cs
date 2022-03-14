﻿////////////////////////////////////////////////////////////////////////////
//
// FlashCap - Independent camera capture library.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using FlashCap.Devices;
using FlashCap.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlashCap
{
    public sealed class CaptureDevices : ICaptureDevices
    {
        public IEnumerable<ICaptureDeviceDescriptor> EnumerateDescriptors() =>
            NativeMethods.GetRuntimePlatform() switch
            {
                NativeMethods.Platforms.Windows =>
                    new DirectShowDevices().EnumerateDescriptors().
                    Concat(new VideoForWindowsDevices().EnumerateDescriptors()),
                NativeMethods.Platforms.Linux =>
                    // TODO: V2L2
                    ArrayEx.Empty<CaptureDeviceDescriptor>(),
                _ =>
                    ArrayEx.Empty<CaptureDeviceDescriptor>(),
            };
    }
}
