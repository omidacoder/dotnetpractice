/* eslint-disable prettier/prettier */
import DefaultLayout from "@/layouts/default";
import LoginForm from "@/components/login-form";
export default function LoginPage() {
  return (
    <DefaultLayout>
      <section className="flex items-center justify-center gap-4 py-8 md:py-10">
          <LoginForm />
      </section>
    </DefaultLayout>
  );
}
