	.arch	armv8-a
	.file	"compressed_assemblies.arm64-v8a.arm64-v8a.s"
	.include	"compressed_assemblies.arm64-v8a-data.inc"

	.section	.data.compressed_assembly_descriptors,"aw",@progbits
	.type	.L.compressed_assembly_descriptors, @object
	.p2align	3
.L.compressed_assembly_descriptors:
	/* 0: App1.Android.dll */
	/* uncompressed_file_size */
	.word	288256
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_0

	/* 1: App1.dll */
	/* uncompressed_file_size */
	.word	181760
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_1

	/* 2: ExoPlayer.Core.dll */
	/* uncompressed_file_size */
	.word	3623424
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_2

	/* 3: ExoPlayer.Dash.dll */
	/* uncompressed_file_size */
	.word	317952
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_3

	/* 4: ExoPlayer.Ext.MediaSession.dll */
	/* uncompressed_file_size */
	.word	189440
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_4

	/* 5: ExoPlayer.Hls.dll */
	/* uncompressed_file_size */
	.word	228352
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_5

	/* 6: ExoPlayer.SmoothStreaming.dll */
	/* uncompressed_file_size */
	.word	121856
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_6

	/* 7: ExoPlayer.UI.dll */
	/* uncompressed_file_size */
	.word	430080
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_7

	/* 8: ExoPlayer.dll */
	/* uncompressed_file_size */
	.word	15872
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_8

	/* 9: FormsViewGroup.dll */
	/* uncompressed_file_size */
	.word	23664
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_9

	/* 10: Java.Interop.dll */
	/* uncompressed_file_size */
	.word	207752
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_10

	/* 11: MediaManager.Forms.dll */
	/* uncompressed_file_size */
	.word	160768
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_11

	/* 12: MediaManager.dll */
	/* uncompressed_file_size */
	.word	261120
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_12

	/* 13: Mono.Android.dll */
	/* uncompressed_file_size */
	.word	26493824
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_13

	/* 14: Mono.Security.dll */
	/* uncompressed_file_size */
	.word	251264
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_14

	/* 15: Plugin.CurrentActivity.dll */
	/* uncompressed_file_size */
	.word	9216
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_15

	/* 16: Plugin.Geolocator.dll */
	/* uncompressed_file_size */
	.word	40960
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_16

	/* 17: Plugin.Media.dll */
	/* uncompressed_file_size */
	.word	64000
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_17

	/* 18: Plugin.Permissions.dll */
	/* uncompressed_file_size */
	.word	18944
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_18

	/* 19: System.ComponentModel.Composition.dll */
	/* uncompressed_file_size */
	.word	267656
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_19

	/* 20: System.Core.dll */
	/* uncompressed_file_size */
	.word	1094024
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_20

	/* 21: System.Data.DataSetExtensions.dll */
	/* uncompressed_file_size */
	.word	38792
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_21

	/* 22: System.Data.dll */
	/* uncompressed_file_size */
	.word	1935752
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_22

	/* 23: System.Drawing.Common.dll */
	/* uncompressed_file_size */
	.word	170376
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_23

	/* 24: System.IO.Compression.FileSystem.dll */
	/* uncompressed_file_size */
	.word	27528
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_24

	/* 25: System.IO.Compression.dll */
	/* uncompressed_file_size */
	.word	124808
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_25

	/* 26: System.Net.Http.dll */
	/* uncompressed_file_size */
	.word	291712
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_26

	/* 27: System.Numerics.Vectors.dll */
	/* uncompressed_file_size */
	.word	21384
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_27

	/* 28: System.Numerics.dll */
	/* uncompressed_file_size */
	.word	128904
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_28

	/* 29: System.Runtime.Serialization.dll */
	/* uncompressed_file_size */
	.word	875912
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_29

	/* 30: System.Runtime.dll */
	/* uncompressed_file_size */
	.word	21384
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_30

	/* 31: System.ServiceModel.Internals.dll */
	/* uncompressed_file_size */
	.word	227720
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_31

	/* 32: System.Transactions.dll */
	/* uncompressed_file_size */
	.word	41352
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_32

	/* 33: System.Web.Services.dll */
	/* uncompressed_file_size */
	.word	223624
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_33

	/* 34: System.Xml.Linq.dll */
	/* uncompressed_file_size */
	.word	146824
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_34

	/* 35: System.Xml.dll */
	/* uncompressed_file_size */
	.word	2451848
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_35

	/* 36: System.dll */
	/* uncompressed_file_size */
	.word	1969032
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_36

	/* 37: Xamarin.Android.Arch.Core.Common.dll */
	/* uncompressed_file_size */
	.word	37520
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_37

	/* 38: Xamarin.Android.Arch.Core.Runtime.dll */
	/* uncompressed_file_size */
	.word	27280
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_38

	/* 39: Xamarin.Android.Arch.Lifecycle.Common.dll */
	/* uncompressed_file_size */
	.word	55960
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_39

	/* 40: Xamarin.Android.Arch.Lifecycle.LiveData.Core.dll */
	/* uncompressed_file_size */
	.word	36520
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_40

	/* 41: Xamarin.Android.Arch.Lifecycle.LiveData.dll */
	/* uncompressed_file_size */
	.word	31392
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_41

	/* 42: Xamarin.Android.Arch.Lifecycle.Runtime.dll */
	/* uncompressed_file_size */
	.word	31392
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_42

	/* 43: Xamarin.Android.Arch.Lifecycle.ViewModel.dll */
	/* uncompressed_file_size */
	.word	34976
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_43

	/* 44: Xamarin.Android.Support.Animated.Vector.Drawable.dll */
	/* uncompressed_file_size */
	.word	75952
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_44

	/* 45: Xamarin.Android.Support.Annotations.dll */
	/* uncompressed_file_size */
	.word	160408
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_45

	/* 46: Xamarin.Android.Support.AsyncLayoutInflater.dll */
	/* uncompressed_file_size */
	.word	29352
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_46

	/* 47: Xamarin.Android.Support.Collections.dll */
	/* uncompressed_file_size */
	.word	98968
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_47

	/* 48: Xamarin.Android.Support.Compat.dll */
	/* uncompressed_file_size */
	.word	1849488
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_48

	/* 49: Xamarin.Android.Support.CoordinaterLayout.dll */
	/* uncompressed_file_size */
	.word	135840
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_49

	/* 50: Xamarin.Android.Support.Core.UI.dll */
	/* uncompressed_file_size */
	.word	47248
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_50

	/* 51: Xamarin.Android.Support.Core.Utils.dll */
	/* uncompressed_file_size */
	.word	24216
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_51

	/* 52: Xamarin.Android.Support.CursorAdapter.dll */
	/* uncompressed_file_size */
	.word	65688
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_52

	/* 53: Xamarin.Android.Support.CustomTabs.dll */
	/* uncompressed_file_size */
	.word	199320
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_53

	/* 54: Xamarin.Android.Support.CustomView.dll */
	/* uncompressed_file_size */
	.word	87192
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_54

	/* 55: Xamarin.Android.Support.Design.dll */
	/* uncompressed_file_size */
	.word	1385616
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_55

	/* 56: Xamarin.Android.Support.DocumentFile.dll */
	/* uncompressed_file_size */
	.word	35992
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_56

	/* 57: Xamarin.Android.Support.DrawerLayout.dll */
	/* uncompressed_file_size */
	.word	87192
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_57

	/* 58: Xamarin.Android.Support.Fragment.dll */
	/* uncompressed_file_size */
	.word	389784
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_58

	/* 59: Xamarin.Android.Support.Interpolator.dll */
	/* uncompressed_file_size */
	.word	26784
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_59

	/* 60: Xamarin.Android.Support.Loader.dll */
	/* uncompressed_file_size */
	.word	91792
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_60

	/* 61: Xamarin.Android.Support.LocalBroadcastManager.dll */
	/* uncompressed_file_size */
	.word	23728
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_61

	/* 62: Xamarin.Android.Support.Media.Compat.dll */
	/* uncompressed_file_size */
	.word	798880
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_62

	/* 63: Xamarin.Android.Support.Print.dll */
	/* uncompressed_file_size */
	.word	37008
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_63

	/* 64: Xamarin.Android.Support.SlidingPaneLayout.dll */
	/* uncompressed_file_size */
	.word	65696
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_64

	/* 65: Xamarin.Android.Support.SwipeRefreshLayout.dll */
	/* uncompressed_file_size */
	.word	83624
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_65

	/* 66: Xamarin.Android.Support.Transition.dll */
	/* uncompressed_file_size */
	.word	313496
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_66

	/* 67: Xamarin.Android.Support.Vector.Drawable.dll */
	/* uncompressed_file_size */
	.word	62112
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_67

	/* 68: Xamarin.Android.Support.VersionedParcelable.dll */
	/* uncompressed_file_size */
	.word	116392
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_68

	/* 69: Xamarin.Android.Support.ViewPager.dll */
	/* uncompressed_file_size */
	.word	131728
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_69

	/* 70: Xamarin.Android.Support.v4.dll */
	/* uncompressed_file_size */
	.word	41096
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_70

	/* 71: Xamarin.Android.Support.v7.AppCompat.dll */
	/* uncompressed_file_size */
	.word	2103960
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_71

	/* 72: Xamarin.Android.Support.v7.CardView.dll */
	/* uncompressed_file_size */
	.word	58520
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_72

	/* 73: Xamarin.Android.Support.v7.MediaRouter.dll */
	/* uncompressed_file_size */
	.word	840864
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_73

	/* 74: Xamarin.Android.Support.v7.Palette.dll */
	/* uncompressed_file_size */
	.word	67736
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_74

	/* 75: Xamarin.Android.Support.v7.RecyclerView.dll */
	/* uncompressed_file_size */
	.word	882848
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_75

	/* 76: Xamarin.Essentials.dll */
	/* uncompressed_file_size */
	.word	209800
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_76

	/* 77: Xamarin.Forms.Core.dll */
	/* uncompressed_file_size */
	.word	933496
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_77

	/* 78: Xamarin.Forms.Maps.Android.dll */
	/* uncompressed_file_size */
	.word	32392
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_78

	/* 79: Xamarin.Forms.Maps.dll */
	/* uncompressed_file_size */
	.word	29304
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_79

	/* 80: Xamarin.Forms.Platform.Android.dll */
	/* uncompressed_file_size */
	.word	674304
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_80

	/* 81: Xamarin.Forms.Platform.dll */
	/* uncompressed_file_size */
	.word	19072
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_81

	/* 82: Xamarin.Forms.Xaml.dll */
	/* uncompressed_file_size */
	.word	109688
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_82

	/* 83: Xamarin.GooglePlayServices.Base.dll */
	/* uncompressed_file_size */
	.word	603728
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_83

	/* 84: Xamarin.GooglePlayServices.Basement.dll */
	/* uncompressed_file_size */
	.word	547928
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_84

	/* 85: Xamarin.GooglePlayServices.Maps.dll */
	/* uncompressed_file_size */
	.word	563792
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_85

	/* 86: Xamarin.GooglePlayServices.Tasks.dll */
	/* uncompressed_file_size */
	.word	95320
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_86

	/* 87: mscorlib.dll */
	/* uncompressed_file_size */
	.word	4514184
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_87

	/* 88: netstandard.dll */
	/* uncompressed_file_size */
	.word	99720
	/* loaded */
	.byte	0
	/* data */
	.zero	3
	.xword	compressed_assembly_data_88

	.size	.L.compressed_assembly_descriptors, 1424
	.section	.data.compressed_assemblies,"aw",@progbits
	.type	compressed_assemblies, @object
	.p2align	3
	.global	compressed_assemblies
compressed_assemblies:
	/* count */
	.word	89
	/* descriptors */
	.zero	4
	.xword	.L.compressed_assembly_descriptors
	.size	compressed_assemblies, 16
