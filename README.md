## Online Job Finder 

> Online Job Finder မှာ အဓိက ရည်ရွယ်ချက်က Admins, Recuriters, Applicants တွေနဲတူ အလုပ်လျှောက်လို့တွေကို တင်လို့ရမယ် အလုပ်တွေ Resume တွေတင်ပြီး အလုပ်တွေကိုအလွယ်တကူ လျှောက်လို့ရအောင် ဒီ system ကိုရေးသားထားပါတယ်

 ---------------------------------

Tech Stack

- Backend() - .Net(EFCore) API, MSSQL

Backend Repository ကို ဒီ [Link](https://github.com/one-project-one-month/online-job-finder-csharp) ကနေ စမ်းလို့ရပါတယ်

---------------------------------

ပါ၀င်တဲ့ Table တွေကတော့
1. Roles
2. Users Applicant Profiles Company Profiles
3. Skills, Job_Categories, Locations
4. Jobs, Job_Skills
5. Applicant Skills, Applicant Job Categories, Applicant Experiences, Applicant Education
6. Resumes, Applications
7. Social Media, Saved Jobs, Reviews

---
## Contributors

---

**Description**

Online Job Finder App ကို ကိုလင်းရဲ့ ဦးဆောင်မှုဖြင့် စတင်ခဲ့ပြီး တစ်လအတွင်းပြီးနိုင်‌‌လောက်သည်အထိ scope သတ်မှတ်ခဲ့ပါတယ်။ OJF မှာက Recuriters တွေက အလုပ်တွေကိုလာရောက်တင်နိုင်မယ် ပြီးတော့ Applicants တွေက အလုပ်တွေကိုလာလျှောက်နိုင်မှာပါတယ်။ OJF မှာက အလုပ်တွေရဲ့ အမျိုးအစားတွေကိုခွဲခြားထားပြီး နေရပ်လိပ်စာ။ အလုပ်လျှောက်လိုတဲ့ သူများအတွက်လည်း တစ်ခါတည်း Profile တွေကို Profile Pictures, Personal Details, Expected and Current Salaries, Educations Background, Experiences,... စသည်ဖြင့် မိမိတို့သဘောကျသလို ပြုလုပ်နိုင်ပြီးတော့ Resume အနေနဲ အလုပ်တွေကိုပို့ပြီး အလွယ်တကူလျှောက်ထားနိုင်မှာပါတယ်။ လိုအပ်တဲ့အခါမှာလဲ Hard skills, Soft Skills, Locations, Roles, တို့အပါအဝင် နောက်ပိုင်းမှာ အသစ်ပေါ်ပေါက်လာနိုင်မယ် အလုပ်အမျိုးအစားတွေကိုပါ မိမိတို့သဘောအတိုင်း အသစ်ထပ်တိုင်းအောင်ပါ အဆင်သင့်ရေးသားပေးထားပါတယ်ခင်ဗျာ။


### Users
>  Admins, Recuriters, Applicants ရဲ့ အချက်အလက်များ သိမ်းရန်၊ Roles နဲ့ Users ချိတ်ဆက်ပေးရန်

```
	User_Id uniqueidentifier
	Role_Id uniqueidentifier
 Username nvarchar(max) 
	Profile_Photo nvarchar(max)
	Email nvarchar(max) 
	PasswordHash nvarchar(max) 
	RefreshToken nvarchar(max) 
	RefreshTokenExpiryTime datetime2(7) 
	Is_Information_Completed bit 
```

### Roles
>  Roles က Users တွေရဲ့ Roles တွေကိုသိမ်းထားပေးရန်

```
	Role_Id uniqueidentifier 
	Role_Name nvarchar(max) 
```

### Saved Jobs
>  Jobs ကိုတွေခဲ့ပြီး သဘောကျခဲ့တဲ့ အလုပ်တွေကို Saved ထားပြီး ပြန်ရှာဖို့ရတာလွယ်ကူစေရန် 

```
Saved_Jobs_Id uniqueidentifier 
Jobs_Id uniqueidentifier 
Applicant_Profiles_Id uniqueidentifier 
Status nvarchar(max) 
```

### Skills
>  Skills လိုအပ်တဲ့ Skills တွေကိုသိမ်းထားရန်

```
Skills_Id uniqueidentifier 
Skills_Name nvarchar(max) 
Description nvarchar(max) 
```

### Social Media
>  Social Media က Users အချင်းချင်း contact လုပ်ရတာလွယ်ကူစေရန်

```
Social_Media_Id uniqueidentifier 
User_Id uniqueidentifier 
link nvarchar(max) 
```

### Reviews
>  Reviews က Applicants တွေ Company တွေကို Reviews တွေပေးနိုင်ရန်

```
Reviews_Id uniqueidentifier 
Company_Profiles_Id uniqueidentifier 
Applicant_Profiles_Id uniqueidentifier 
Ratings decimal(20, 2) 
Comments text 
```

### Resumes
>  Resumes က Applicants တွေက Company တွေကို Resumes အလွယ်တကူပို့ပေးနိုင်ရန်

```
Resumes_Id uniqueidentifier 
User_Id uniqueidentifier 
File_Path nvarchar(max) 
```

### Locations
>  Locations လိုအပ်တဲ့ Locations တွေကိုသိမ်းထားရန်

```
Location_Id uniqueidentifier 
Location_Name nvarchar(max) 
Description nvarchar(max) 
```

### Jobs
>  Jobs က Recuriters တွေက Jobs တွေပိုစ့်တွေတာရတာ လွယ်ကူစေရန်

```
Jobs_Id uniqueidentifier 
Company_Profiles_Id uniqueidentifier 
Job_Categories_Id uniqueidentifier 
Location_Id uniqueidentifier 
Title nvarchar(max) 
Type nvarchar(max) 
Description nvarchar(max) 
Requirements text 
Num_Of_Posts int 
Salary decimal(20, 2) 
Address nvarchar(max) 
Status nvarchar(max) 
```

### Job Skills
>  Job Skills က Applicants တွေတင်တဲ့အခါ အလွယ်တကူဖြည့်နိုင်ရန်

```
Job_Skills_Id uniqueidentifier 
Jobs_Id uniqueidentifier 
Skills_Id uniqueidentifier 
Extra_Skills nvarchar(max) 
```

### Job Categories
>  Job Categories က Applicants တွေတင်တဲ့အခါ အလွယ်တကူဖြည့်နိုင်ရန်

```
Job_Categories_Id uniqueidentifier 
Industry nvarchar(max) 
Description nvarchar(max) 
```

### Company Profiles
>  Company Profiles လိုအပ်တဲ့ Company Profiles Data တွေကိုသိမ်းထားရန်

```
Company_Profiles_Id uniqueidentifier 
User_Id uniqueidentifier 
Location_Id uniqueidentifier 
Company_Name nvarchar(max) 
Phone nvarchar(max) 
Website nvarchar(max) 
Address nvarchar(max) 
Description nvarchar(max) 
```

### Applications
>  Applications က Jobs တွေပိုစ့်တွေကို အလုပ်လျှောက်ရတာလွယ်တကူ မြန်ဆန်စေရန်

```
Applications_Id uniqueidentifier 
Jobs_Id uniqueidentifier 
Applicant_Profiles_Id uniqueidentifier 
Resumes_Id uniqueidentifier 
Status nvarchar(max) 
```

### Applicant Skills
> Applicant Skills က Applicants တွေတင်တဲ့ Resume ရေးတဲ့အခါ အလွယ်တကူဖြည့်နိုင်ရန်

```
Applicant_Skills_Id uniqueidentifier
Applicant_Profiles_Id uniqueidentifier
Skills_Id uniqueidentifier
Extra_Skills nvarchar(max) 
```

### Applicant Profiles
> Applicant Profiles က Applicants တွေတင်တဲ့ Resume ရေးတဲ့အခါ အလွယ်တကူဖြည့်နိုင်ရန်

```
	Applicant_Profiles_Id uniqueidentifier
	User_Id uniqueidentifier
	Location_Id uniqueidentifier
	FullName nvarchar(max)
	Phone nvarchar(max)
	Address nvarchar(max)
	Description nvarchar(max) 
```

### Applicant Job Categories
> Applicant Job Categories က Applicants တွေတင်တဲ့ Job ရှာတဲ့အခါ အလွယ်တကူ ရှာနိုင်ရန်

```
	Applicant_Job_Categories_Id uniqueidentifier
	Applicant_Profiles_Id uniqueidentifier
	Job_Categories_Id uniqueidentifier
	Reasons nvarchar(max) 
```

### Applicant Experiences
> Applicant Experiences က Applicants တွေတင်တဲ့ Resume ရေးတဲ့အခါ အလွယ်တကူဖြည့်နိုင်ရန်

```
	Applicant_Experiences_Id uniqueidentifier
	Applicant_Profiles_Id uniqueidentifier
	Company_Name nvarchar(max)
	Location nvarchar(max)
	Title nvarchar(max)
	Description nvarchar(max) 
	Job_Type nvarchar(max)
	Start_Date datetime2(7)
	End_Date datetime2(7)
	Currently_Working bit
```

### Applicant Educations
> Applicant Educations က Applicants တွေတင်တဲ့ Resume ရေးတဲ့အခါ အလွယ်တကူဖြည့်နိုင်ရန်

```
Applicant_Educations_Id uniqueidentifier
Applicant_Profiles_Id uniqueidentifier
School_Name nvarchar(max)
Degree nvarchar(max)
Field_Of_Study nvarchar(max)
Description text 
Start_Date datetime2(7)
End_Date datetime2(7)
Still_Attending bit
```

