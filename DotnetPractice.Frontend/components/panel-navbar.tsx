/* eslint-disable prettier/prettier */
import { Navbar, NavbarContent, NavbarItem } from "@heroui/react";
import { CgProfile } from "react-icons/cg";
import { useState } from "react";
import { useEffect } from "react";

import { ThemeSwitch } from "./theme-switch";

import SideMenu from "@/components/sidemenu";

export default function PanelNavbar() {
  const [name, setName] = useState<string | null>(null);

  useEffect(() => {
    return setName(localStorage.getItem("name"));
  }, []);

  return (
    <>
      <Navbar className="shadow">
        <NavbarContent justify="start">
          <SideMenu />
        </NavbarContent>
        {name ? <NavbarContent justify="end">
          <NavbarItem className="hidden sm:flex gap-2">
            <ThemeSwitch />
          </NavbarItem>
          <NavbarItem>
          <div className="w-full grid grid-cols-12 gap-2 items-center">
            <div className="col-span-4">
                <CgProfile size={30} />
            </div>
            <div className="col-span-8">
              <p>{name}</p>
            </div>
          </div>
          </NavbarItem>
        </NavbarContent> : <></>}
      </Navbar>
    </>
  );
}
