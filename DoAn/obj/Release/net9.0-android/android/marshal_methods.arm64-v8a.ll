; ModuleID = 'marshal_methods.arm64-v8a.ll'
source_filename = "marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [137 x ptr] zeroinitializer, align 8

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [411 x i64] [
	i64 u0x0071cf2d27b7d61e, ; 0: lib_Xamarin.AndroidX.SwipeRefreshLayout.dll.so => 73
	i64 u0x02123411c4e01926, ; 1: lib_Xamarin.AndroidX.Navigation.Runtime.dll.so => 69
	i64 u0x022e81ea9c46e03a, ; 2: lib_CommunityToolkit.Maui.Core.dll.so => 35
	i64 u0x02abedc11addc1ed, ; 3: lib_Mono.Android.Runtime.dll.so => 135
	i64 u0x032267b2a94db371, ; 4: lib_Xamarin.AndroidX.AppCompat.dll.so => 52
	i64 u0x043032f1d071fae0, ; 5: ru/Microsoft.Maui.Controls.resources => 24
	i64 u0x044440a55165631e, ; 6: lib-cs-Microsoft.Maui.Controls.resources.dll.so => 2
	i64 u0x046eb1581a80c6b0, ; 7: vi/Microsoft.Maui.Controls.resources => 30
	i64 u0x0517ef04e06e9f76, ; 8: System.Net.Primitives => 107
	i64 u0x0565d18c6da3de38, ; 9: Xamarin.AndroidX.RecyclerView => 71
	i64 u0x0581db89237110e9, ; 10: lib_System.Collections.dll.so => 85
	i64 u0x05989cb940b225a9, ; 11: Microsoft.Maui.dll => 47
	i64 u0x06076b5d2b581f08, ; 12: zh-HK/Microsoft.Maui.Controls.resources => 31
	i64 u0x0680a433c781bb3d, ; 13: Xamarin.AndroidX.Collection.Jvm => 55
	i64 u0x07c57877c7ba78ad, ; 14: ru/Microsoft.Maui.Controls.resources.dll => 24
	i64 u0x07dcdc7460a0c5e4, ; 15: System.Collections.NonGeneric => 83
	i64 u0x08f3c9788ee2153c, ; 16: Xamarin.AndroidX.DrawerLayout => 60
	i64 u0x0919c28b89381a0b, ; 17: lib_Microsoft.Extensions.Options.dll.so => 43
	i64 u0x092266563089ae3e, ; 18: lib_System.Collections.NonGeneric.dll.so => 83
	i64 u0x09d144a7e214d457, ; 19: System.Security.Cryptography => 120
	i64 u0x0b3b632c3bbee20c, ; 20: sk/Microsoft.Maui.Controls.resources => 25
	i64 u0x0b6aff547b84fbe9, ; 21: Xamarin.KotlinX.Serialization.Core.Jvm => 79
	i64 u0x0be2e1f8ce4064ed, ; 22: Xamarin.AndroidX.ViewPager => 74
	i64 u0x0c3ca6cc978e2aae, ; 23: pt-BR/Microsoft.Maui.Controls.resources => 21
	i64 u0x0c59ad9fbbd43abe, ; 24: Mono.Android => 136
	i64 u0x0c7790f60165fc06, ; 25: lib_Microsoft.Maui.Essentials.dll.so => 48
	i64 u0x0e14e73a54dda68e, ; 26: lib_System.Net.NameResolution.dll.so => 106
	i64 u0x102a31b45304b1da, ; 27: Xamarin.AndroidX.CustomView => 59
	i64 u0x10f6cfcbcf801616, ; 28: System.IO.Compression.Brotli => 98
	i64 u0x125b7f94acb989db, ; 29: Xamarin.AndroidX.RecyclerView.dll => 71
	i64 u0x13a01de0cbc3f06c, ; 30: lib-fr-Microsoft.Maui.Controls.resources.dll.so => 8
	i64 u0x13f1e5e209e91af4, ; 31: lib_Java.Interop.dll.so => 134
	i64 u0x13f1e880c25d96d1, ; 32: he/Microsoft.Maui.Controls.resources => 9
	i64 u0x143d8ea60a6a4011, ; 33: Microsoft.Extensions.DependencyInjection.Abstractions => 40
	i64 u0x152a448bd1e745a7, ; 34: Microsoft.Win32.Primitives => 81
	i64 u0x16bf2a22df043a09, ; 35: System.IO.Pipes.dll => 101
	i64 u0x17b56e25558a5d36, ; 36: lib-hu-Microsoft.Maui.Controls.resources.dll.so => 12
	i64 u0x17f9358913beb16a, ; 37: System.Text.Encodings.Web => 123
	i64 u0x18402a709e357f3b, ; 38: lib_Xamarin.KotlinX.Serialization.Core.Jvm.dll.so => 79
	i64 u0x18f0ce884e87d89a, ; 39: nb/Microsoft.Maui.Controls.resources.dll => 18
	i64 u0x1a91866a319e9259, ; 40: lib_System.Collections.Concurrent.dll.so => 82
	i64 u0x1aac34d1917ba5d3, ; 41: lib_System.dll.so => 132
	i64 u0x1aad60783ffa3e5b, ; 42: lib-th-Microsoft.Maui.Controls.resources.dll.so => 27
	i64 u0x1c753b5ff15bce1b, ; 43: Mono.Android.Runtime.dll => 135
	i64 u0x1e3d87657e9659bc, ; 44: Xamarin.AndroidX.Navigation.UI => 70
	i64 u0x1e71143913d56c10, ; 45: lib-ko-Microsoft.Maui.Controls.resources.dll.so => 16
	i64 u0x1ed8fcce5e9b50a0, ; 46: Microsoft.Extensions.Options.dll => 43
	i64 u0x209375905fcc1bad, ; 47: lib_System.IO.Compression.Brotli.dll.so => 98
	i64 u0x20fab3cf2dfbc8df, ; 48: lib_System.Diagnostics.Process.dll.so => 92
	i64 u0x2174319c0d835bc9, ; 49: System.Runtime => 119
	i64 u0x220fd4f2e7c48170, ; 50: th/Microsoft.Maui.Controls.resources => 27
	i64 u0x224538d85ed15a82, ; 51: System.IO.Pipes => 101
	i64 u0x237be844f1f812c7, ; 52: System.Threading.Thread.dll => 126
	i64 u0x2407aef2bbe8fadf, ; 53: System.Console => 89
	i64 u0x240abe014b27e7d3, ; 54: Xamarin.AndroidX.Core.dll => 57
	i64 u0x252073cc3caa62c2, ; 55: fr/Microsoft.Maui.Controls.resources.dll => 8
	i64 u0x2662c629b96b0b30, ; 56: lib_Xamarin.Kotlin.StdLib.dll.so => 77
	i64 u0x268c1439f13bcc29, ; 57: lib_Microsoft.Extensions.Primitives.dll.so => 44
	i64 u0x273f3515de5faf0d, ; 58: id/Microsoft.Maui.Controls.resources.dll => 13
	i64 u0x2742545f9094896d, ; 59: hr/Microsoft.Maui.Controls.resources => 11
	i64 u0x27b410442fad6cf1, ; 60: Java.Interop.dll => 134
	i64 u0x2801845a2c71fbfb, ; 61: System.Net.Primitives.dll => 107
	i64 u0x2a128783efe70ba0, ; 62: uk/Microsoft.Maui.Controls.resources.dll => 29
	i64 u0x2a6507a5ffabdf28, ; 63: System.Diagnostics.TraceSource.dll => 93
	i64 u0x2ad156c8e1354139, ; 64: fi/Microsoft.Maui.Controls.resources => 7
	i64 u0x2af298f63581d886, ; 65: System.Text.RegularExpressions.dll => 125
	i64 u0x2afc1c4f898552ee, ; 66: lib_System.Formats.Asn1.dll.so => 97
	i64 u0x2b148910ed40fbf9, ; 67: zh-Hant/Microsoft.Maui.Controls.resources.dll => 33
	i64 u0x2c8bd14bb93a7d82, ; 68: lib-pl-Microsoft.Maui.Controls.resources.dll.so => 20
	i64 u0x2cbd9262ca785540, ; 69: lib_System.Text.Encoding.CodePages.dll.so => 121
	i64 u0x2d169d318a968379, ; 70: System.Threading.dll => 128
	i64 u0x2d47774b7d993f59, ; 71: sv/Microsoft.Maui.Controls.resources.dll => 26
	i64 u0x2db915caf23548d2, ; 72: System.Text.Json.dll => 124
	i64 u0x2e6f1f226821322a, ; 73: el/Microsoft.Maui.Controls.resources.dll => 5
	i64 u0x2f02f94df3200fe5, ; 74: System.Diagnostics.Process => 92
	i64 u0x2f2e98e1c89b1aff, ; 75: System.Xml.ReaderWriter => 131
	i64 u0x2f5911d9ba814e4e, ; 76: System.Diagnostics.Tracing => 94
	i64 u0x309ee9eeec09a71e, ; 77: lib_Xamarin.AndroidX.Fragment.dll.so => 61
	i64 u0x31195fef5d8fb552, ; 78: _Microsoft.Android.Resource.Designer.dll => 34
	i64 u0x32243413e774362a, ; 79: Xamarin.AndroidX.CardView.dll => 54
	i64 u0x3235427f8d12dae1, ; 80: lib_System.Drawing.Primitives.dll.so => 95
	i64 u0x329753a17a517811, ; 81: fr/Microsoft.Maui.Controls.resources => 8
	i64 u0x32aa989ff07a84ff, ; 82: lib_System.Xml.ReaderWriter.dll.so => 131
	i64 u0x33a31443733849fe, ; 83: lib-es-Microsoft.Maui.Controls.resources.dll.so => 6
	i64 u0x341abc357fbb4ebf, ; 84: lib_System.Net.Sockets.dll.so => 110
	i64 u0x34dfd74fe2afcf37, ; 85: Microsoft.Maui => 47
	i64 u0x34e292762d9615df, ; 86: cs/Microsoft.Maui.Controls.resources.dll => 2
	i64 u0x3508234247f48404, ; 87: Microsoft.Maui.Controls => 45
	i64 u0x3549870798b4cd30, ; 88: lib_Xamarin.AndroidX.ViewPager2.dll.so => 75
	i64 u0x355282fc1c909694, ; 89: Microsoft.Extensions.Configuration => 37
	i64 u0x385c17636bb6fe6e, ; 90: Xamarin.AndroidX.CustomView.dll => 59
	i64 u0x38869c811d74050e, ; 91: System.Net.NameResolution.dll => 106
	i64 u0x393c226616977fdb, ; 92: lib_Xamarin.AndroidX.ViewPager.dll.so => 74
	i64 u0x395e37c3334cf82a, ; 93: lib-ca-Microsoft.Maui.Controls.resources.dll.so => 1
	i64 u0x3b860f9932505633, ; 94: lib_System.Text.Encoding.Extensions.dll.so => 122
	i64 u0x3c7c495f58ac5ee9, ; 95: Xamarin.Kotlin.StdLib => 77
	i64 u0x3d196e782ed8c01a, ; 96: System.Data.SqlClient => 50
	i64 u0x3d2b1913edfc08d7, ; 97: lib_System.Threading.ThreadPool.dll.so => 127
	i64 u0x3d9c2a242b040a50, ; 98: lib_Xamarin.AndroidX.Core.dll.so => 57
	i64 u0x407a10bb4bf95829, ; 99: lib_Xamarin.AndroidX.Navigation.Common.dll.so => 67
	i64 u0x41cab042be111c34, ; 100: lib_Xamarin.AndroidX.AppCompat.AppCompatResources.dll.so => 53
	i64 u0x434c4e1d9284cdae, ; 101: Mono.Android.dll => 136
	i64 u0x43950f84de7cc79a, ; 102: pl/Microsoft.Maui.Controls.resources.dll => 20
	i64 u0x4515080865a951a5, ; 103: Xamarin.Kotlin.StdLib.dll => 77
	i64 u0x45c40276a42e283e, ; 104: System.Diagnostics.TraceSource => 93
	i64 u0x46a4213bc97fe5ae, ; 105: lib-ru-Microsoft.Maui.Controls.resources.dll.so => 24
	i64 u0x47daf4e1afbada10, ; 106: pt/Microsoft.Maui.Controls.resources => 22
	i64 u0x49e952f19a4e2022, ; 107: System.ObjectModel => 112
	i64 u0x4a5667b2462a664b, ; 108: lib_Xamarin.AndroidX.Navigation.UI.dll.so => 70
	i64 u0x4b7b6532ded934b7, ; 109: System.Text.Json => 124
	i64 u0x4cc5f15266470798, ; 110: lib_Xamarin.AndroidX.Loader.dll.so => 66
	i64 u0x4d479f968a05e504, ; 111: System.Linq.Expressions.dll => 102
	i64 u0x4d55a010ffc4faff, ; 112: System.Private.Xml => 114
	i64 u0x4d95fccc1f67c7ca, ; 113: System.Runtime.Loader.dll => 116
	i64 u0x4dcf44c3c9b076a2, ; 114: it/Microsoft.Maui.Controls.resources.dll => 14
	i64 u0x4dd9247f1d2c3235, ; 115: Xamarin.AndroidX.Loader.dll => 66
	i64 u0x4e32f00cb0937401, ; 116: Mono.Android.Runtime => 135
	i64 u0x4e5eea4668ac2b18, ; 117: System.Text.Encoding.CodePages => 121
	i64 u0x4f21ee6ef9eb527e, ; 118: ca/Microsoft.Maui.Controls.resources => 1
	i64 u0x5037f0be3c28c7a3, ; 119: lib_Microsoft.Maui.Controls.dll.so => 45
	i64 u0x5112ed116d87baf8, ; 120: CommunityToolkit.Mvvm => 36
	i64 u0x5131bbe80989093f, ; 121: Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll => 64
	i64 u0x51bb8a2afe774e32, ; 122: System.Drawing => 96
	i64 u0x526ce79eb8e90527, ; 123: lib_System.Net.Primitives.dll.so => 107
	i64 u0x52829f00b4467c38, ; 124: lib_System.Data.Common.dll.so => 90
	i64 u0x529ffe06f39ab8db, ; 125: Xamarin.AndroidX.Core => 57
	i64 u0x52ff996554dbf352, ; 126: Microsoft.Maui.Graphics => 49
	i64 u0x535f7e40e8fef8af, ; 127: lib-sk-Microsoft.Maui.Controls.resources.dll.so => 25
	i64 u0x53c3014b9437e684, ; 128: lib-zh-HK-Microsoft.Maui.Controls.resources.dll.so => 31
	i64 u0x54795225dd1587af, ; 129: lib_System.Runtime.dll.so => 119
	i64 u0x556e8b63b660ab8b, ; 130: Xamarin.AndroidX.Lifecycle.Common.Jvm.dll => 62
	i64 u0x5588627c9a108ec9, ; 131: System.Collections.Specialized => 84
	i64 u0x571c5cfbec5ae8e2, ; 132: System.Private.Uri => 113
	i64 u0x579a06fed6eec900, ; 133: System.Private.CoreLib.dll => 133
	i64 u0x57c542c14049b66d, ; 134: System.Diagnostics.DiagnosticSource => 91
	i64 u0x58601b2dda4a27b9, ; 135: lib-ja-Microsoft.Maui.Controls.resources.dll.so => 15
	i64 u0x58688d9af496b168, ; 136: Microsoft.Extensions.DependencyInjection.dll => 39
	i64 u0x5a89a886ae30258d, ; 137: lib_Xamarin.AndroidX.CoordinatorLayout.dll.so => 56
	i64 u0x5a8f6699f4a1caa9, ; 138: lib_System.Threading.dll.so => 128
	i64 u0x5ae9cd33b15841bf, ; 139: System.ComponentModel => 88
	i64 u0x5b5f0e240a06a2a2, ; 140: da/Microsoft.Maui.Controls.resources.dll => 3
	i64 u0x5bf46332cc09e9b2, ; 141: lib_System.Data.SqlClient.dll.so => 50
	i64 u0x5c393624b8176517, ; 142: lib_Microsoft.Extensions.Logging.dll.so => 41
	i64 u0x5db0cbbd1028510e, ; 143: lib_System.Runtime.InteropServices.dll.so => 115
	i64 u0x5db30905d3e5013b, ; 144: Xamarin.AndroidX.Collection.Jvm.dll => 55
	i64 u0x5e467bc8f09ad026, ; 145: System.Collections.Specialized.dll => 84
	i64 u0x5ea92fdb19ec8c4c, ; 146: System.Text.Encodings.Web.dll => 123
	i64 u0x5eb8046dd40e9ac3, ; 147: System.ComponentModel.Primitives => 86
	i64 u0x5f36ccf5c6a57e24, ; 148: System.Xml.ReaderWriter.dll => 131
	i64 u0x5f4294b9b63cb842, ; 149: System.Data.Common => 90
	i64 u0x5f9a2d823f664957, ; 150: lib-el-Microsoft.Maui.Controls.resources.dll.so => 5
	i64 u0x609f4b7b63d802d4, ; 151: lib_Microsoft.Extensions.DependencyInjection.dll.so => 39
	i64 u0x60cd4e33d7e60134, ; 152: Xamarin.KotlinX.Coroutines.Core.Jvm => 78
	i64 u0x60f62d786afcf130, ; 153: System.Memory => 104
	i64 u0x61be8d1299194243, ; 154: Microsoft.Maui.Controls.Xaml => 46
	i64 u0x61d2cba29557038f, ; 155: de/Microsoft.Maui.Controls.resources => 4
	i64 u0x61d88f399afb2f45, ; 156: lib_System.Runtime.Loader.dll.so => 116
	i64 u0x622eef6f9e59068d, ; 157: System.Private.CoreLib => 133
	i64 u0x6400f68068c1e9f1, ; 158: Xamarin.Google.Android.Material.dll => 76
	i64 u0x65ecac39144dd3cc, ; 159: Microsoft.Maui.Controls.dll => 45
	i64 u0x65ece51227bfa724, ; 160: lib_System.Runtime.Numerics.dll.so => 117
	i64 u0x6692e924eade1b29, ; 161: lib_System.Console.dll.so => 89
	i64 u0x66a4e5c6a3fb0bae, ; 162: lib_Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll.so => 64
	i64 u0x66d13304ce1a3efa, ; 163: Xamarin.AndroidX.CursorAdapter => 58
	i64 u0x68558ec653afa616, ; 164: lib-da-Microsoft.Maui.Controls.resources.dll.so => 3
	i64 u0x6872ec7a2e36b1ac, ; 165: System.Drawing.Primitives.dll => 95
	i64 u0x68fbbbe2eb455198, ; 166: System.Formats.Asn1 => 97
	i64 u0x69063fc0ba8e6bdd, ; 167: he/Microsoft.Maui.Controls.resources.dll => 9
	i64 u0x6a4d7577b2317255, ; 168: System.Runtime.InteropServices.dll => 115
	i64 u0x6ace3b74b15ee4a4, ; 169: nb/Microsoft.Maui.Controls.resources => 18
	i64 u0x6d12bfaa99c72b1f, ; 170: lib_Microsoft.Maui.Graphics.dll.so => 49
	i64 u0x6d79993361e10ef2, ; 171: Microsoft.Extensions.Primitives => 44
	i64 u0x6d86d56b84c8eb71, ; 172: lib_Xamarin.AndroidX.CursorAdapter.dll.so => 58
	i64 u0x6d9bea6b3e895cf7, ; 173: Microsoft.Extensions.Primitives.dll => 44
	i64 u0x6e25a02c3833319a, ; 174: lib_Xamarin.AndroidX.Navigation.Fragment.dll.so => 68
	i64 u0x6fd2265da78b93a4, ; 175: lib_Microsoft.Maui.dll.so => 47
	i64 u0x6fdfc7de82c33008, ; 176: cs/Microsoft.Maui.Controls.resources => 2
	i64 u0x70e99f48c05cb921, ; 177: tr/Microsoft.Maui.Controls.resources.dll => 28
	i64 u0x70fd3deda22442d2, ; 178: lib-nb-Microsoft.Maui.Controls.resources.dll.so => 18
	i64 u0x71a495ea3761dde8, ; 179: lib-it-Microsoft.Maui.Controls.resources.dll.so => 14
	i64 u0x71ad672adbe48f35, ; 180: System.ComponentModel.Primitives.dll => 86
	i64 u0x72b1fb4109e08d7b, ; 181: lib-hr-Microsoft.Maui.Controls.resources.dll.so => 11
	i64 u0x73e4ce94e2eb6ffc, ; 182: lib_System.Memory.dll.so => 104
	i64 u0x755a91767330b3d4, ; 183: lib_Microsoft.Extensions.Configuration.dll.so => 37
	i64 u0x76012e7334db86e5, ; 184: lib_Xamarin.AndroidX.SavedState.dll.so => 72
	i64 u0x76ca07b878f44da0, ; 185: System.Runtime.Numerics.dll => 117
	i64 u0x780bc73597a503a9, ; 186: lib-ms-Microsoft.Maui.Controls.resources.dll.so => 17
	i64 u0x783606d1e53e7a1a, ; 187: th/Microsoft.Maui.Controls.resources.dll => 27
	i64 u0x78a45e51311409b6, ; 188: Xamarin.AndroidX.Fragment.dll => 61
	i64 u0x7adb8da2ac89b647, ; 189: fi/Microsoft.Maui.Controls.resources.dll => 7
	i64 u0x7bef86a4335c4870, ; 190: System.ComponentModel.TypeConverter => 87
	i64 u0x7c0820144cd34d6a, ; 191: sk/Microsoft.Maui.Controls.resources.dll => 25
	i64 u0x7c2a0bd1e0f988fc, ; 192: lib-de-Microsoft.Maui.Controls.resources.dll.so => 4
	i64 u0x7cc637f941f716d0, ; 193: CommunityToolkit.Maui.Core => 35
	i64 u0x7d649b75d580bb42, ; 194: ms/Microsoft.Maui.Controls.resources.dll => 17
	i64 u0x7d8ee2bdc8e3aad1, ; 195: System.Numerics.Vectors => 111
	i64 u0x7dfc3d6d9d8d7b70, ; 196: System.Collections => 85
	i64 u0x7e2e564fa2f76c65, ; 197: lib_System.Diagnostics.Tracing.dll.so => 94
	i64 u0x7e946809d6008ef2, ; 198: lib_System.ObjectModel.dll.so => 112
	i64 u0x7ecc13347c8fd849, ; 199: lib_System.ComponentModel.dll.so => 88
	i64 u0x7f00ddd9b9ca5a13, ; 200: Xamarin.AndroidX.ViewPager.dll => 74
	i64 u0x7f9351cd44b1273f, ; 201: Microsoft.Extensions.Configuration.Abstractions => 38
	i64 u0x7fbd557c99b3ce6f, ; 202: lib_Xamarin.AndroidX.Lifecycle.LiveData.Core.dll.so => 63
	i64 u0x812c069d5cdecc17, ; 203: System.dll => 132
	i64 u0x81ab745f6c0f5ce6, ; 204: zh-Hant/Microsoft.Maui.Controls.resources => 33
	i64 u0x8277f2be6b5ce05f, ; 205: Xamarin.AndroidX.AppCompat => 52
	i64 u0x828f06563b30bc50, ; 206: lib_Xamarin.AndroidX.CardView.dll.so => 54
	i64 u0x82df8f5532a10c59, ; 207: lib_System.Drawing.dll.so => 96
	i64 u0x82f6403342e12049, ; 208: uk/Microsoft.Maui.Controls.resources => 29
	i64 u0x83c14ba66c8e2b8c, ; 209: zh-Hans/Microsoft.Maui.Controls.resources => 32
	i64 u0x84ae73148a4557d2, ; 210: lib_System.IO.Pipes.dll.so => 101
	i64 u0x86a909228dc7657b, ; 211: lib-zh-Hant-Microsoft.Maui.Controls.resources.dll.so => 33
	i64 u0x86b3e00c36b84509, ; 212: Microsoft.Extensions.Configuration.dll => 37
	i64 u0x87c69b87d9283884, ; 213: lib_System.Threading.Thread.dll.so => 126
	i64 u0x87f6569b25707834, ; 214: System.IO.Compression.Brotli.dll => 98
	i64 u0x8842b3a5d2d3fb36, ; 215: Microsoft.Maui.Essentials => 48
	i64 u0x88bda98e0cffb7a9, ; 216: lib_Xamarin.KotlinX.Coroutines.Core.Jvm.dll.so => 78
	i64 u0x897a606c9e39c75f, ; 217: lib_System.ComponentModel.Primitives.dll.so => 86
	i64 u0x8ad229ea26432ee2, ; 218: Xamarin.AndroidX.Loader => 66
	i64 u0x8b4ff5d0fdd5faa1, ; 219: lib_System.Diagnostics.DiagnosticSource.dll.so => 91
	i64 u0x8b8d01333a96d0b5, ; 220: System.Diagnostics.Process.dll => 92
	i64 u0x8b9ceca7acae3451, ; 221: lib-he-Microsoft.Maui.Controls.resources.dll.so => 9
	i64 u0x8d0f420977c2c1c7, ; 222: Xamarin.AndroidX.CursorAdapter.dll => 58
	i64 u0x8d7b8ab4b3310ead, ; 223: System.Threading => 128
	i64 u0x8da188285aadfe8e, ; 224: System.Collections.Concurrent => 82
	i64 u0x8ed807bfe9858dfc, ; 225: Xamarin.AndroidX.Navigation.Common => 67
	i64 u0x8ee08b8194a30f48, ; 226: lib-hi-Microsoft.Maui.Controls.resources.dll.so => 10
	i64 u0x8ef7601039857a44, ; 227: lib-ro-Microsoft.Maui.Controls.resources.dll.so => 23
	i64 u0x8f32c6f611f6ffab, ; 228: pt/Microsoft.Maui.Controls.resources.dll => 22
	i64 u0x8f8829d21c8985a4, ; 229: lib-pt-BR-Microsoft.Maui.Controls.resources.dll.so => 21
	i64 u0x90263f8448b8f572, ; 230: lib_System.Diagnostics.TraceSource.dll.so => 93
	i64 u0x903101b46fb73a04, ; 231: _Microsoft.Android.Resource.Designer => 34
	i64 u0x90393bd4865292f3, ; 232: lib_System.IO.Compression.dll.so => 99
	i64 u0x90634f86c5ebe2b5, ; 233: Xamarin.AndroidX.Lifecycle.ViewModel.Android => 64
	i64 u0x907b636704ad79ef, ; 234: lib_Microsoft.Maui.Controls.Xaml.dll.so => 46
	i64 u0x91418dc638b29e68, ; 235: lib_Xamarin.AndroidX.CustomView.dll.so => 59
	i64 u0x9157bd523cd7ed36, ; 236: lib_System.Text.Json.dll.so => 124
	i64 u0x918659c09fba7095, ; 237: lib_DoAn.dll.so => 80
	i64 u0x91a74f07b30d37e2, ; 238: System.Linq.dll => 103
	i64 u0x91fa41a87223399f, ; 239: ca/Microsoft.Maui.Controls.resources.dll => 1
	i64 u0x93cfa73ab28d6e35, ; 240: ms/Microsoft.Maui.Controls.resources => 17
	i64 u0x944077d8ca3c6580, ; 241: System.IO.Compression.dll => 99
	i64 u0x967fc325e09bfa8c, ; 242: es/Microsoft.Maui.Controls.resources => 6
	i64 u0x9732d8dbddea3d9a, ; 243: id/Microsoft.Maui.Controls.resources => 13
	i64 u0x978be80e5210d31b, ; 244: Microsoft.Maui.Graphics.dll => 49
	i64 u0x97b8c771ea3e4220, ; 245: System.ComponentModel.dll => 88
	i64 u0x97e144c9d3c6976e, ; 246: System.Collections.Concurrent.dll => 82
	i64 u0x991d510397f92d9d, ; 247: System.Linq.Expressions => 102
	i64 u0x999cb19e1a04ffd3, ; 248: CommunityToolkit.Mvvm.dll => 36
	i64 u0x99a00ca5270c6878, ; 249: Xamarin.AndroidX.Navigation.Runtime => 69
	i64 u0x99cdc6d1f2d3a72f, ; 250: ko/Microsoft.Maui.Controls.resources.dll => 16
	i64 u0x9b211a749105beac, ; 251: System.Transactions.Local => 129
	i64 u0x9d5dbcf5a48583fe, ; 252: lib_Xamarin.AndroidX.Activity.dll.so => 51
	i64 u0x9d74dee1a7725f34, ; 253: Microsoft.Extensions.Configuration.Abstractions.dll => 38
	i64 u0x9e4534b6adaf6e84, ; 254: nl/Microsoft.Maui.Controls.resources => 19
	i64 u0x9eaf1efdf6f7267e, ; 255: Xamarin.AndroidX.Navigation.Common.dll => 67
	i64 u0x9ef542cf1f78c506, ; 256: Xamarin.AndroidX.Lifecycle.LiveData.Core => 63
	i64 u0xa0d8259f4cc284ec, ; 257: lib_System.Security.Cryptography.dll.so => 120
	i64 u0xa1440773ee9d341e, ; 258: Xamarin.Google.Android.Material => 76
	i64 u0xa1b9d7c27f47219f, ; 259: Xamarin.AndroidX.Navigation.UI.dll => 70
	i64 u0xa2572680829d2c7c, ; 260: System.IO.Pipelines.dll => 100
	i64 u0xa46aa1eaa214539b, ; 261: ko/Microsoft.Maui.Controls.resources => 16
	i64 u0xa4d20d2ff0563d26, ; 262: lib_CommunityToolkit.Mvvm.dll.so => 36
	i64 u0xa4edc8f2ceae241a, ; 263: System.Data.Common.dll => 90
	i64 u0xa5494f40f128ce6a, ; 264: System.Runtime.Serialization.Formatters.dll => 118
	i64 u0xa5e599d1e0524750, ; 265: System.Numerics.Vectors.dll => 111
	i64 u0xa5f1ba49b85dd355, ; 266: System.Security.Cryptography.dll => 120
	i64 u0xa67dbee13e1df9ca, ; 267: Xamarin.AndroidX.SavedState.dll => 72
	i64 u0xa68a420042bb9b1f, ; 268: Xamarin.AndroidX.DrawerLayout.dll => 60
	i64 u0xa701630d14073ade, ; 269: DoAn.dll => 80
	i64 u0xa763fbb98df8d9fb, ; 270: lib_Microsoft.Win32.Primitives.dll.so => 81
	i64 u0xa78ce3745383236a, ; 271: Xamarin.AndroidX.Lifecycle.Common.Jvm => 62
	i64 u0xa7c31b56b4dc7b33, ; 272: hu/Microsoft.Maui.Controls.resources => 12
	i64 u0xa964304b5631e28a, ; 273: CommunityToolkit.Maui.Core.dll => 35
	i64 u0xaa2219c8e3449ff5, ; 274: Microsoft.Extensions.Logging.Abstractions => 42
	i64 u0xaa443ac34067eeef, ; 275: System.Private.Xml.dll => 114
	i64 u0xaa52de307ef5d1dd, ; 276: System.Net.Http => 105
	i64 u0xaaaf86367285a918, ; 277: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 40
	i64 u0xaaf84bb3f052a265, ; 278: el/Microsoft.Maui.Controls.resources => 5
	i64 u0xab9c1b2687d86b0b, ; 279: lib_System.Linq.Expressions.dll.so => 102
	i64 u0xac2af3fa195a15ce, ; 280: System.Runtime.Numerics => 117
	i64 u0xac5376a2a538dc10, ; 281: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 63
	i64 u0xacd46e002c3ccb97, ; 282: ro/Microsoft.Maui.Controls.resources => 23
	i64 u0xad89c07347f1bad6, ; 283: nl/Microsoft.Maui.Controls.resources.dll => 19
	i64 u0xadbb53caf78a79d2, ; 284: System.Web.HttpUtility => 130
	i64 u0xadc90ab061a9e6e4, ; 285: System.ComponentModel.TypeConverter.dll => 87
	i64 u0xadf511667bef3595, ; 286: System.Net.Security => 109
	i64 u0xae282bcd03739de7, ; 287: Java.Interop => 134
	i64 u0xae53579c90db1107, ; 288: System.ObjectModel.dll => 112
	i64 u0xafe29f45095518e7, ; 289: lib_Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll.so => 65
	i64 u0xb05cc42cd94c6d9d, ; 290: lib-sv-Microsoft.Maui.Controls.resources.dll.so => 26
	i64 u0xb0bb43dc52ea59f9, ; 291: System.Diagnostics.Tracing.dll => 94
	i64 u0xb220631954820169, ; 292: System.Text.RegularExpressions => 125
	i64 u0xb2a3f67f3bf29fce, ; 293: da/Microsoft.Maui.Controls.resources => 3
	i64 u0xb3f0a0fcda8d3ebc, ; 294: Xamarin.AndroidX.CardView => 54
	i64 u0xb46be1aa6d4fff93, ; 295: hi/Microsoft.Maui.Controls.resources => 10
	i64 u0xb477491be13109d8, ; 296: ar/Microsoft.Maui.Controls.resources => 0
	i64 u0xb4bd7015ecee9d86, ; 297: System.IO.Pipelines => 100
	i64 u0xb5c7fcdafbc67ee4, ; 298: Microsoft.Extensions.Logging.Abstractions.dll => 42
	i64 u0xb5ea31d5244c6626, ; 299: System.Threading.ThreadPool.dll => 127
	i64 u0xb7212c4683a94afe, ; 300: System.Drawing.Primitives => 95
	i64 u0xb7b7753d1f319409, ; 301: sv/Microsoft.Maui.Controls.resources => 26
	i64 u0xb81a2c6e0aee50fe, ; 302: lib_System.Private.CoreLib.dll.so => 133
	i64 u0xb9f64d3b230def68, ; 303: lib-pt-Microsoft.Maui.Controls.resources.dll.so => 22
	i64 u0xb9fc3c8a556e3691, ; 304: ja/Microsoft.Maui.Controls.resources => 15
	i64 u0xba48785529705af9, ; 305: System.Collections.dll => 85
	i64 u0xbb65706fde942ce3, ; 306: System.Net.Sockets => 110
	i64 u0xbbd180354b67271a, ; 307: System.Runtime.Serialization.Formatters => 118
	i64 u0xbd0e2c0d55246576, ; 308: System.Net.Http.dll => 105
	i64 u0xbd437a2cdb333d0d, ; 309: Xamarin.AndroidX.ViewPager2 => 75
	i64 u0xbee38d4a88835966, ; 310: Xamarin.AndroidX.AppCompat.AppCompatResources => 53
	i64 u0xc040a4ab55817f58, ; 311: ar/Microsoft.Maui.Controls.resources.dll => 0
	i64 u0xc0d928351ab5ca77, ; 312: System.Console.dll => 89
	i64 u0xc12b8b3afa48329c, ; 313: lib_System.Linq.dll.so => 103
	i64 u0xc1ff9ae3cdb6e1e6, ; 314: Xamarin.AndroidX.Activity.dll => 51
	i64 u0xc28c50f32f81cc73, ; 315: ja/Microsoft.Maui.Controls.resources.dll => 15
	i64 u0xc2bcfec99f69365e, ; 316: Xamarin.AndroidX.ViewPager2.dll => 75
	i64 u0xc418342d4d013afc, ; 317: DoAn => 80
	i64 u0xc4d3858ed4d08512, ; 318: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 65
	i64 u0xc50fded0ded1418c, ; 319: lib_System.ComponentModel.TypeConverter.dll.so => 87
	i64 u0xc519125d6bc8fb11, ; 320: lib_System.Net.Requests.dll.so => 108
	i64 u0xc5293b19e4dc230e, ; 321: Xamarin.AndroidX.Navigation.Fragment => 68
	i64 u0xc5325b2fcb37446f, ; 322: lib_System.Private.Xml.dll.so => 114
	i64 u0xc5a0f4b95a699af7, ; 323: lib_System.Private.Uri.dll.so => 113
	i64 u0xc7c01e7d7c93a110, ; 324: System.Text.Encoding.Extensions.dll => 122
	i64 u0xc7ce851898a4548e, ; 325: lib_System.Web.HttpUtility.dll.so => 130
	i64 u0xc858a28d9ee5a6c5, ; 326: lib_System.Collections.Specialized.dll.so => 84
	i64 u0xca3a723e7342c5b6, ; 327: lib-tr-Microsoft.Maui.Controls.resources.dll.so => 28
	i64 u0xcab3493c70141c2d, ; 328: pl/Microsoft.Maui.Controls.resources => 20
	i64 u0xcacfddc9f7c6de76, ; 329: ro/Microsoft.Maui.Controls.resources.dll => 23
	i64 u0xcbd4fdd9cef4a294, ; 330: lib__Microsoft.Android.Resource.Designer.dll.so => 34
	i64 u0xcc2876b32ef2794c, ; 331: lib_System.Text.RegularExpressions.dll.so => 125
	i64 u0xcc5c3bb714c4561e, ; 332: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 78
	i64 u0xcc76886e09b88260, ; 333: Xamarin.KotlinX.Serialization.Core.Jvm.dll => 79
	i64 u0xccf25c4b634ccd3a, ; 334: zh-Hans/Microsoft.Maui.Controls.resources.dll => 32
	i64 u0xcd10a42808629144, ; 335: System.Net.Requests => 108
	i64 u0xcdd0c48b6937b21c, ; 336: Xamarin.AndroidX.SwipeRefreshLayout => 73
	i64 u0xcf23d8093f3ceadf, ; 337: System.Diagnostics.DiagnosticSource.dll => 91
	i64 u0xd1194e1d8a8de83c, ; 338: lib_Xamarin.AndroidX.Lifecycle.Common.Jvm.dll.so => 62
	i64 u0xd333d0af9e423810, ; 339: System.Runtime.InteropServices => 115
	i64 u0xd3426d966bb704f5, ; 340: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 53
	i64 u0xd3651b6fc3125825, ; 341: System.Private.Uri.dll => 113
	i64 u0xd373685349b1fe8b, ; 342: Microsoft.Extensions.Logging.dll => 41
	i64 u0xd3e4c8d6a2d5d470, ; 343: it/Microsoft.Maui.Controls.resources => 14
	i64 u0xd4645626dffec99d, ; 344: lib_Microsoft.Extensions.DependencyInjection.Abstractions.dll.so => 40
	i64 u0xd5507e11a2b2839f, ; 345: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 65
	i64 u0xd6694f8359737e4e, ; 346: Xamarin.AndroidX.SavedState => 72
	i64 u0xd6d21782156bc35b, ; 347: Xamarin.AndroidX.SwipeRefreshLayout.dll => 73
	i64 u0xd72329819cbbbc44, ; 348: lib_Microsoft.Extensions.Configuration.Abstractions.dll.so => 38
	i64 u0xd7b3764ada9d341d, ; 349: lib_Microsoft.Extensions.Logging.Abstractions.dll.so => 42
	i64 u0xda1dfa4c534a9251, ; 350: Microsoft.Extensions.DependencyInjection => 39
	i64 u0xdad05a11827959a3, ; 351: System.Collections.NonGeneric.dll => 83
	i64 u0xdb5383ab5865c007, ; 352: lib-vi-Microsoft.Maui.Controls.resources.dll.so => 30
	i64 u0xdbeda89f832aa805, ; 353: vi/Microsoft.Maui.Controls.resources.dll => 30
	i64 u0xdbf2a779fbc3ac31, ; 354: System.Transactions.Local.dll => 129
	i64 u0xdbf9607a441b4505, ; 355: System.Linq => 103
	i64 u0xdc75032002d1a212, ; 356: lib_System.Transactions.Local.dll.so => 129
	i64 u0xdce2c53525640bf3, ; 357: Microsoft.Extensions.Logging => 41
	i64 u0xdd2b722d78ef5f43, ; 358: System.Runtime.dll => 119
	i64 u0xdd67031857c72f96, ; 359: lib_System.Text.Encodings.Web.dll.so => 123
	i64 u0xdde30e6b77aa6f6c, ; 360: lib-zh-Hans-Microsoft.Maui.Controls.resources.dll.so => 32
	i64 u0xde8769ebda7d8647, ; 361: hr/Microsoft.Maui.Controls.resources.dll => 11
	i64 u0xe0142572c095a480, ; 362: Xamarin.AndroidX.AppCompat.dll => 52
	i64 u0xe02f89350ec78051, ; 363: Xamarin.AndroidX.CoordinatorLayout.dll => 56
	i64 u0xe192a588d4410686, ; 364: lib_System.IO.Pipelines.dll.so => 100
	i64 u0xe1a08bd3fa539e0d, ; 365: System.Runtime.Loader => 116
	i64 u0xe1ecfdb7fff86067, ; 366: System.Net.Security.dll => 109
	i64 u0xe2420585aeceb728, ; 367: System.Net.Requests.dll => 108
	i64 u0xe29b73bc11392966, ; 368: lib-id-Microsoft.Maui.Controls.resources.dll.so => 13
	i64 u0xe2e426c7714fa0bc, ; 369: Microsoft.Win32.Primitives.dll => 81
	i64 u0xe3811d68d4fe8463, ; 370: pt-BR/Microsoft.Maui.Controls.resources.dll => 21
	i64 u0xe494f7ced4ecd10a, ; 371: hu/Microsoft.Maui.Controls.resources.dll => 12
	i64 u0xe4a9b1e40d1e8917, ; 372: lib-fi-Microsoft.Maui.Controls.resources.dll.so => 7
	i64 u0xe5434e8a119ceb69, ; 373: lib_Mono.Android.dll.so => 136
	i64 u0xe89a2a9ef110899b, ; 374: System.Drawing.dll => 96
	i64 u0xedc4817167106c23, ; 375: System.Net.Sockets.dll => 110
	i64 u0xedc632067fb20ff3, ; 376: System.Memory.dll => 104
	i64 u0xedc8e4ca71a02a8b, ; 377: Xamarin.AndroidX.Navigation.Runtime.dll => 69
	i64 u0xee81f5b3f1c4f83b, ; 378: System.Threading.ThreadPool => 127
	i64 u0xeeb7ebb80150501b, ; 379: lib_Xamarin.AndroidX.Collection.Jvm.dll.so => 55
	i64 u0xef03b1b5a04e9709, ; 380: System.Text.Encoding.CodePages.dll => 121
	i64 u0xef72742e1bcca27a, ; 381: Microsoft.Maui.Essentials.dll => 48
	i64 u0xefec0b7fdc57ec42, ; 382: Xamarin.AndroidX.Activity => 51
	i64 u0xf00c29406ea45e19, ; 383: es/Microsoft.Maui.Controls.resources.dll => 6
	i64 u0xf09e47b6ae914f6e, ; 384: System.Net.NameResolution => 106
	i64 u0xf11b621fc87b983f, ; 385: Microsoft.Maui.Controls.Xaml.dll => 46
	i64 u0xf1c4b4005493d871, ; 386: System.Formats.Asn1.dll => 97
	i64 u0xf238bd79489d3a96, ; 387: lib-nl-Microsoft.Maui.Controls.resources.dll.so => 19
	i64 u0xf37221fda4ef8830, ; 388: lib_Xamarin.Google.Android.Material.dll.so => 76
	i64 u0xf3ddfe05336abf29, ; 389: System => 132
	i64 u0xf4c1dd70a5496a17, ; 390: System.IO.Compression => 99
	i64 u0xf6077741019d7428, ; 391: Xamarin.AndroidX.CoordinatorLayout => 56
	i64 u0xf77b20923f07c667, ; 392: de/Microsoft.Maui.Controls.resources.dll => 4
	i64 u0xf7e2cac4c45067b3, ; 393: lib_System.Numerics.Vectors.dll.so => 111
	i64 u0xf7e74930e0e3d214, ; 394: zh-HK/Microsoft.Maui.Controls.resources.dll => 31
	i64 u0xf84773b5c81e3cef, ; 395: lib-uk-Microsoft.Maui.Controls.resources.dll.so => 29
	i64 u0xf8e045dc345b2ea3, ; 396: lib_Xamarin.AndroidX.RecyclerView.dll.so => 71
	i64 u0xf915dc29808193a1, ; 397: System.Web.HttpUtility.dll => 130
	i64 u0xf96c777a2a0686f4, ; 398: hi/Microsoft.Maui.Controls.resources.dll => 10
	i64 u0xf9eec5bb3a6aedc6, ; 399: Microsoft.Extensions.Options => 43
	i64 u0xfa3f278f288b0e84, ; 400: lib_System.Net.Security.dll.so => 109
	i64 u0xfa5ed7226d978949, ; 401: lib-ar-Microsoft.Maui.Controls.resources.dll.so => 0
	i64 u0xfa645d91e9fc4cba, ; 402: System.Threading.Thread => 126
	i64 u0xfbf0a31c9fc34bc4, ; 403: lib_System.Net.Http.dll.so => 105
	i64 u0xfc6b7527cc280b3f, ; 404: lib_System.Runtime.Serialization.Formatters.dll.so => 118
	i64 u0xfc719aec26adf9d9, ; 405: Xamarin.AndroidX.Navigation.Fragment.dll => 68
	i64 u0xfcd5b90cf101e36b, ; 406: System.Data.SqlClient.dll => 50
	i64 u0xfd22f00870e40ae0, ; 407: lib_Xamarin.AndroidX.DrawerLayout.dll.so => 60
	i64 u0xfd536c702f64dc47, ; 408: System.Text.Encoding.Extensions => 122
	i64 u0xfd583f7657b6a1cb, ; 409: Xamarin.AndroidX.Fragment => 61
	i64 u0xfeae9952cf03b8cb ; 410: tr/Microsoft.Maui.Controls.resources => 28
], align 8

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [411 x i32] [
	i32 73, i32 69, i32 35, i32 135, i32 52, i32 24, i32 2, i32 30,
	i32 107, i32 71, i32 85, i32 47, i32 31, i32 55, i32 24, i32 83,
	i32 60, i32 43, i32 83, i32 120, i32 25, i32 79, i32 74, i32 21,
	i32 136, i32 48, i32 106, i32 59, i32 98, i32 71, i32 8, i32 134,
	i32 9, i32 40, i32 81, i32 101, i32 12, i32 123, i32 79, i32 18,
	i32 82, i32 132, i32 27, i32 135, i32 70, i32 16, i32 43, i32 98,
	i32 92, i32 119, i32 27, i32 101, i32 126, i32 89, i32 57, i32 8,
	i32 77, i32 44, i32 13, i32 11, i32 134, i32 107, i32 29, i32 93,
	i32 7, i32 125, i32 97, i32 33, i32 20, i32 121, i32 128, i32 26,
	i32 124, i32 5, i32 92, i32 131, i32 94, i32 61, i32 34, i32 54,
	i32 95, i32 8, i32 131, i32 6, i32 110, i32 47, i32 2, i32 45,
	i32 75, i32 37, i32 59, i32 106, i32 74, i32 1, i32 122, i32 77,
	i32 50, i32 127, i32 57, i32 67, i32 53, i32 136, i32 20, i32 77,
	i32 93, i32 24, i32 22, i32 112, i32 70, i32 124, i32 66, i32 102,
	i32 114, i32 116, i32 14, i32 66, i32 135, i32 121, i32 1, i32 45,
	i32 36, i32 64, i32 96, i32 107, i32 90, i32 57, i32 49, i32 25,
	i32 31, i32 119, i32 62, i32 84, i32 113, i32 133, i32 91, i32 15,
	i32 39, i32 56, i32 128, i32 88, i32 3, i32 50, i32 41, i32 115,
	i32 55, i32 84, i32 123, i32 86, i32 131, i32 90, i32 5, i32 39,
	i32 78, i32 104, i32 46, i32 4, i32 116, i32 133, i32 76, i32 45,
	i32 117, i32 89, i32 64, i32 58, i32 3, i32 95, i32 97, i32 9,
	i32 115, i32 18, i32 49, i32 44, i32 58, i32 44, i32 68, i32 47,
	i32 2, i32 28, i32 18, i32 14, i32 86, i32 11, i32 104, i32 37,
	i32 72, i32 117, i32 17, i32 27, i32 61, i32 7, i32 87, i32 25,
	i32 4, i32 35, i32 17, i32 111, i32 85, i32 94, i32 112, i32 88,
	i32 74, i32 38, i32 63, i32 132, i32 33, i32 52, i32 54, i32 96,
	i32 29, i32 32, i32 101, i32 33, i32 37, i32 126, i32 98, i32 48,
	i32 78, i32 86, i32 66, i32 91, i32 92, i32 9, i32 58, i32 128,
	i32 82, i32 67, i32 10, i32 23, i32 22, i32 21, i32 93, i32 34,
	i32 99, i32 64, i32 46, i32 59, i32 124, i32 80, i32 103, i32 1,
	i32 17, i32 99, i32 6, i32 13, i32 49, i32 88, i32 82, i32 102,
	i32 36, i32 69, i32 16, i32 129, i32 51, i32 38, i32 19, i32 67,
	i32 63, i32 120, i32 76, i32 70, i32 100, i32 16, i32 36, i32 90,
	i32 118, i32 111, i32 120, i32 72, i32 60, i32 80, i32 81, i32 62,
	i32 12, i32 35, i32 42, i32 114, i32 105, i32 40, i32 5, i32 102,
	i32 117, i32 63, i32 23, i32 19, i32 130, i32 87, i32 109, i32 134,
	i32 112, i32 65, i32 26, i32 94, i32 125, i32 3, i32 54, i32 10,
	i32 0, i32 100, i32 42, i32 127, i32 95, i32 26, i32 133, i32 22,
	i32 15, i32 85, i32 110, i32 118, i32 105, i32 75, i32 53, i32 0,
	i32 89, i32 103, i32 51, i32 15, i32 75, i32 80, i32 65, i32 87,
	i32 108, i32 68, i32 114, i32 113, i32 122, i32 130, i32 84, i32 28,
	i32 20, i32 23, i32 34, i32 125, i32 78, i32 79, i32 32, i32 108,
	i32 73, i32 91, i32 62, i32 115, i32 53, i32 113, i32 41, i32 14,
	i32 40, i32 65, i32 72, i32 73, i32 38, i32 42, i32 39, i32 83,
	i32 30, i32 30, i32 129, i32 103, i32 129, i32 41, i32 119, i32 123,
	i32 32, i32 11, i32 52, i32 56, i32 100, i32 116, i32 109, i32 108,
	i32 13, i32 81, i32 21, i32 12, i32 7, i32 136, i32 96, i32 110,
	i32 104, i32 69, i32 127, i32 55, i32 121, i32 48, i32 51, i32 6,
	i32 106, i32 46, i32 97, i32 19, i32 76, i32 132, i32 99, i32 56,
	i32 4, i32 111, i32 31, i32 29, i32 71, i32 130, i32 10, i32 43,
	i32 109, i32 0, i32 126, i32 105, i32 118, i32 68, i32 50, i32 60,
	i32 122, i32 61, i32 28
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 u0x0000000000000000, ; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { memory(write, argmem: none, inaccessiblemem: none) "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+fix-cortex-a53-835769,+neon,+outline-atomics,+v8a" }

; Metadata
!llvm.module.flags = !{!0, !1, !7, !8, !9, !10}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!".NET for Android remotes/origin/release/9.0.1xx @ 278e101698269c9bc8840aa94d72e7f24066a96d"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"branch-target-enforcement", i32 0}
!8 = !{i32 1, !"sign-return-address", i32 0}
!9 = !{i32 1, !"sign-return-address-all", i32 0}
!10 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
