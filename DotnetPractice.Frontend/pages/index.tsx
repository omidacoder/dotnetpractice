import { Link } from "@heroui/react";
import { button as buttonStyles } from "@heroui/react";

import { siteConfig } from "@/config/site";
import { title, subtitle } from "@/components/primitives";
import DefaultLayout from "@/layouts/default";

export default function IndexPage() {
  return (
    <DefaultLayout>
      <section className="flex flex-col items-center justify-center gap-4 py-8 md:py-10">
        <div className="inline-block max-w-xl text-center justify-center">
          <span className={title()}>
            A client UI for ASP dotnet core backend
          </span>
          <div className={subtitle({ class: "mt-4" })}>
            This a simple project for practice purpose
          </div>
        </div>

        <div className="flex gap-3">
          <Link
            className={buttonStyles({
              color: "primary",
              radius: "full",
              variant: "shadow",
            })}
            href={siteConfig.links.signup}
          >
            Register
          </Link>
          <Link
            className={buttonStyles({ variant: "bordered", radius: "full" })}
            href={siteConfig.links.login}
          >
            Login
          </Link>
        </div>
      </section>
    </DefaultLayout>
  );
}
